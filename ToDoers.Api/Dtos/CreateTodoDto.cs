using System.ComponentModel.DataAnnotations;

namespace ToDoers.Api.Dtos
{
    public record class CreateTodoDto(
        
        [Required] String Text,
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        int TagId,
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        int Priority,
        DateOnly Deadline
    );
}
