namespace ToDoers.Api.Data
{
    using Microsoft.EntityFrameworkCore;
    using ToDoers.Api.Entities;

    /// <summary>
    /// Defines the <see cref="TodoContext" />
    /// </summary>
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets the Todos
        /// </summary>
        public DbSet<Todo> Todos => Set<Todo>();

        /// <summary>
        /// Gets the Tags
        /// </summary>
        public DbSet<Tag> Tags => Set<Tag>();

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Tag>().HasData(
                // Initial
                new Tag { Id = 1, Name = "Work" },
                new Tag { Id = 2, Name = "Personal" },
                new Tag { Id = 3, Name = "Urgent" },

                // Work extensions
                new Tag { Id = 4, Name = "Code Review" },
                new Tag { Id = 5, Name = "Refactor" },
                new Tag { Id = 6, Name = "Debug" },
                new Tag { Id = 7, Name = "Deployment" },
                new Tag { Id = 8, Name = "Research" },

                // Personal/Wellness
                new Tag { Id = 9, Name = "Errands" },
                new Tag { Id = 10, Name = "Finance" },
                new Tag { Id = 11, Name = "Health" },
                new Tag { Id = 12, Name = "Learning" },

                // Productivity
                new Tag { Id = 13, Name = "Blocked" },
                new Tag { Id = 14, Name = "Recurring" },
                new Tag { Id = 15, Name = "QuickWin" }
            );
        }
    }
}
