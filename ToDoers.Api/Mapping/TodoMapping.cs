using ToDoers.Api.Dtos;
using ToDoers.Api.Entities;

namespace ToDoers.Api.Mapping
{
    public static class TodoMapping
    {
        public static TodoSummaryDto ToSummaryDto(this Todo todo)
        {
            return new TodoSummaryDto(
                todo.Id,
                todo.Text,
                todo.Tag!.Name,
                todo.Priority,
                todo.Deadline
            );
        }
        public static TodoDetailsDto ToDetailDto(this Todo todo)
        {
            return new TodoDetailsDto(
                todo.Id,
                todo.Text,
                todo.TagId,
                todo.Priority,
                todo.Deadline
            );
        }
        public static Todo ToEntity(this CreateTodoDto todo)
        {
            return new Todo
            {
                Text = todo.Text,
                TagId = todo.TagId,
                Priority = todo.Priority,
                Deadline = todo.Deadline
            };
        }

        public static Todo ToEntity(this UpdateTodoDto todo, int id)
        {
            return new Todo
            {
                Id = id,
                Text = todo.Text,
                TagId = todo.TagId,
                Priority = todo.Priority,
                Deadline = todo.Deadline
            };
        }
    }
}
