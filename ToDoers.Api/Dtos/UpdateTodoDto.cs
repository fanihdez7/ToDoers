namespace ToDoers.Api.Dtos
{
    public record class UpdateTodoDto(
        String Text,
        String Tag,
        int Priority,
        DateOnly Deadline
    );
    
}
