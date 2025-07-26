namespace ToDoers.Api.Endpoints
{
    using ToDoers.Api.Dtos;

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
        /// Defines the ToDos
        /// </summary>
        private static readonly List<TodoDto> ToDos =
        [
            new (
                1,
                "Finish the course",
                "C#; Programming; .net8",
                2,
                new DateOnly(2025,9,1)
            ),
            new (
                2,
                "Develop first app ToDoers",
                "C#; Programming; .net8; angular",
                1,
                new DateOnly(2025,8,22)
            )
        ];

        /// <summary>
        /// The MapTodosEndpoints
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/></param>
        /// <returns>The <see cref="RouteGroupBuilder"/></returns>
        public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("todos").WithParameterValidation();

            

            group.MapGet("/", () => ToDos);

            group.MapGet("/{id}", (int id) =>
            {

                TodoDto? todo = ToDos.Find(todo => todo.Id == id);
                return todo is null ? Results.NotFound() : Results.Ok(todo);

            }).WithName(GetToDoEndpointName);

            group.MapPost("/", (CreateTodoDto newToDo) =>
            {
                TodoDto ToDo = new(
                    ToDos.Count + 1,
                    newToDo.Text,
                    newToDo.Tag,
                    newToDo.Priority,
                    newToDo.Deadline
                );
                ToDos.Add(ToDo);

                return Results.CreatedAtRoute(GetToDoEndpointName, new { id = ToDo.Id }, ToDo);
            });

            group.MapPut("/{id}", (int id, UpdateTodoDto update) =>
            {

                var index = ToDos.FindIndex(todo => todo.Id == id);
                if (index == -1)
                {
                    return Results.NotFound();
                }
                ToDos[index] = new TodoDto(
                    id,
                    update.Text,
                    update.Tag,
                    update.Priority,
                    update.Deadline
                );
                return Results.NoContent();
            });

            group.MapDelete("/{id}", (int id) =>
            {
                ToDos.RemoveAll(todo => todo.Id == id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
