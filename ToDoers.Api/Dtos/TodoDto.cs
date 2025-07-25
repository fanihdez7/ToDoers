namespace ToDoers.Api.Dtos
{
    public record class TodoDto(
        int Id, 
        String Text,
        String Tag,
        int Priority,
        DateOnly Deadline
    );
}
