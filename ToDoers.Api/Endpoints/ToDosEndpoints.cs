namespace ToDoers.Api.Endpoints
{
    using Microsoft.EntityFrameworkCore;
    using ToDoers.Api.Data;
    using ToDoers.Api.Dtos;
    using ToDoers.Api.Entities;
    using ToDoers.Api.Mapping;

    /// <summary>
    /// Defines the <see cref="ToDosEndpoints" />
    /// </summary>
    public static class ToDosEndpoints
    {
        /// <summary>
        /// Defines the GetToDoEndpointName
        /// </summary>
        internal const string GetToDoEndpointName = "GetToDo";

        

        /// <summary>
        /// The MapTodosEndpoints
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/></param>
        /// <returns>The <see cref="RouteGroupBuilder"/></returns>
        public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("todos").WithParameterValidation();

            

            group.MapGet("/", async (TodoContext dbContext) =>                
                await dbContext.Todos
                        .Include(todo => todo.Tag)
                        .Select(todo => todo.ToSummaryDto())
                        .AsNoTracking()
                        .ToListAsync()
            );

            group.MapGet("/{id}", async (int id, TodoContext dbContext) =>
            {

                Todo? todo = await dbContext.Todos.FindAsync(id);
                return todo is null ? Results.NotFound() : Results.Ok(todo.ToDetailDto());

            }).WithName(GetToDoEndpointName);

            group.MapPost("/", async (CreateTodoDto newToDo, TodoContext dbContext) =>
            {


                Todo todo = newToDo.ToEntity();                

                dbContext.Todos.Add(todo);
                await dbContext.SaveChangesAsync();
                
                return Results.CreatedAtRoute(GetToDoEndpointName, new { id = todo.Id }, todo.ToDetailDto());
            });

            group.MapPut("/{id}", async (int id, UpdateTodoDto update, TodoContext dbContext) =>
            {
                Todo todo = await dbContext.Todos.FindAsync(id);

                if (todo is null)
                {
                    return Results.NotFound();
                }
                
                dbContext.Entry(todo)
                    .CurrentValues
                    .SetValues(update.ToEntity(id));

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, TodoContext dbContext) =>
            {
                await dbContext.Todos.Where(todo => todo.Id == id).ExecuteDeleteAsync();
                
                return Results.NoContent();
            });

            return group;
        }
    }
}
