using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoers.Api.Data;
using ToDoers.Api.Dtos;
using ToDoers.Api.Entities;
using ToDoers.Api.Services;

namespace ToDoers.UnitTests.Services
{
    public class TodoServiceTests
    {
        

        [Fact]
        public void TodoService_GetTodosAsync_ReturnsTodos() 
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>().Options;            
            var mockSet = new Mock<DbSet<Todo>>();
            var mockDbContext = new Mock<TodoContext>(options);
            mockDbContext.Setup(m => m.Todos).Returns(mockSet.Object);
            var service = new TodoService(mockDbContext.Object);

            // Act
            var actual = service.GetTodosAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Task<IEnumerable<TodoSummaryDto>>>(actual);            
        }
    }
}
