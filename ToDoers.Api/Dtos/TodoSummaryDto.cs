namespace ToDoers.Api.Dtos
{
    public record class TodoSummaryDto(
        int Id, 
        String Text,
        String Tag,
        int Priority,
        DateOnly Deadline
    );
}
