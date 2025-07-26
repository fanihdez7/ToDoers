namespace ToDoers.Api.Dtos
{
    public record class TodoDetailsDto(
        int Id, 
        String Text,
        int TagId,
        int Priority,
        DateOnly Deadline
    );
}
