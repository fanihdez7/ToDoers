using System.ComponentModel.DataAnnotations;

namespace ToDoers.Api.Dtos
{
    public record class UpdateTodoDto(
        [Required] String Text,
        int TagId,
        int Priority,
        DateOnly Deadline
    );
    
}
