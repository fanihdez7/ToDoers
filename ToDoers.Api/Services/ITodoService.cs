using ToDoers.Api.Dtos;

namespace ToDoers.Api.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoSummaryDto>> GetTodosAsync();
        Task<TodoDetailsDto?> GetTodoByIdAsync(int id);
        Task<TodoDetailsDto> CreateTodoAsync(CreateTodoDto todoToCreate);
        Task<TodoDetailsDto?> UpdateTodoAsync(int id, UpdateTodoDto todoToUpdate);
        Task DeleteTodoAsync(int id);
    }
}
