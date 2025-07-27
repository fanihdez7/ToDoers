namespace ToDoers.Api.Endpoints
{
    using Microsoft.EntityFrameworkCore;
    using ToDoers.Api.Data;
    using ToDoers.Api.Dtos;
    using ToDoers.Api.Entities;
    using ToDoers.Api.Mapping;
    using ToDoers.Api.Services;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            

            group.MapGet("/", async (ITodoService todoService) => 
            {
                var result = await todoService.GetTodosAsync();
                return Results.Ok(result);
            });

            group.MapGet("/{id}", async (int id, ITodoService todoService) =>
            {
                var result = await todoService.GetTodoByIdAsync(id);
                return result is null ? Results.NotFound() : Results.Ok(result);

            }).WithName(GetToDoEndpointName);

            group.MapPost("/", async (CreateTodoDto newToDo, ITodoService todoService) =>
            {
                var result = await todoService.CreateTodoAsync(newToDo);                
                return Results.CreatedAtRoute(GetToDoEndpointName, new { id = result.Id }, result);
            });

            group.MapPut("/{id}", async (int id, UpdateTodoDto update, ITodoService todoService) =>
            {
                await todoService.UpdateTodoAsync(id, update);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, ITodoService todoService) =>
            {
                await todoService.DeleteTodoAsync(id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
