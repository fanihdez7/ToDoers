using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using ToDoers.Api.Data;
using ToDoers.Api.Dtos;
using ToDoers.Api.Entities;
using ToDoers.Api.Mapping;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToDoers.Api.Services
{
    public class TodoService : ITodoService
    {

        private readonly TodoContext _dbContext;

        public TodoService(TodoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TodoDetailsDto> CreateTodoAsync(CreateTodoDto todoToCreate)
        {

            Todo todo = todoToCreate.ToEntity();
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();
            return todo.ToDetailDto();
        }

        public async Task<int> DeleteTodoAsync(int id)
        {
            var delete = await _dbContext.Todos.Where(todo => todo.Id == id).ExecuteDeleteAsync();
            return delete;
        }

        public async Task<TodoDetailsDto?> GetTodoByIdAsync(int id)
        {
            Todo? todo = await _dbContext.Todos.FindAsync(id);
            return todo?.ToDetailDto();
        }

        public async Task<IEnumerable<TodoSummaryDto>> GetTodosAsync()
        {
            var result = await _dbContext.Todos
               .Include(todo => todo.Tag)
               .Select(todo => todo.ToSummaryDto())
               .AsNoTracking()
               .ToListAsync();
            return result;
        }

        public async Task<TodoDetailsDto?> UpdateTodoAsync(int id, UpdateTodoDto todoToUpdate)
        {
            Todo? todo = await _dbContext.Todos.FindAsync(id);

            if (todo is null)
            {
                return null;
            }

            Todo updatedTodo = todoToUpdate.ToEntity(id);

            _dbContext.Entry(todo)
                .CurrentValues
                .SetValues(updatedTodo);

            await _dbContext.SaveChangesAsync();
            return updatedTodo.ToDetailDto();
        }
    }
}
