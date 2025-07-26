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

            

            group.MapGet("/", 
                (TodoContext dbContext) =>                
                    dbContext.Todos
                            .Include(todo => todo.Tag)
                            .Select(todo => todo.ToSummaryDto())
                            .AsNoTracking()
                );

            group.MapGet("/{id}", (int id, TodoContext dbContext) =>
            {

                Todo? todo = dbContext.Todos.Find(id);
                return todo is null ? Results.NotFound() : Results.Ok(todo.ToDetailDto());

            }).WithName(GetToDoEndpointName);

            group.MapPost("/", (CreateTodoDto newToDo, TodoContext dbContext) =>
            {


                Todo todo = newToDo.ToEntity();                

                dbContext.Todos.Add(todo);
                dbContext.SaveChanges();
                
                return Results.CreatedAtRoute(GetToDoEndpointName, new { id = todo.Id }, todo.ToDetailDto());
            });

            group.MapPut("/{id}", (int id, UpdateTodoDto update, TodoContext dbContext) =>
            {
                Todo todo = dbContext.Todos.Find(id);

                if (todo is null)
                {
                    return Results.NotFound();
                }
                
                dbContext.Entry(todo)
                    .CurrentValues
                    .SetValues(update.ToEntity(id));

                dbContext.SaveChanges();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", (int id, TodoContext dbContext) =>
            {
                dbContext.Todos.Where(todo => todo.Id == id).ExecuteDelete();
                
                return Results.NoContent();
            });

            return group;
        }
    }
}
