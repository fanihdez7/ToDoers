using Microsoft.EntityFrameworkCore;
using ToDoers.Api.Entities;

namespace ToDoers.Api.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<Tag> Tags => Set<Tag>();
    }
}
