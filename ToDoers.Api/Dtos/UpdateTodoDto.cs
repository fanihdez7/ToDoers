using System.ComponentModel.DataAnnotations;

namespace ToDoers.Api.Dtos
{
    public record class UpdateTodoDto(
        [Required] String Text,
        String Tag,
        int Priority,
        DateOnly Deadline
    );
    
}
