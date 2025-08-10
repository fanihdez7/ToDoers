using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
using static System.Collections.Specialized.BitVector32;

namespace ToDoers.UnitTests.Services
{
    public class TodoServiceTests
    {
        Mock<TodoContext> mockDbContext;
        TodoService service; 
        public TodoServiceTests() {
            var options = new DbContextOptionsBuilder<TodoContext>().Options;
            
            this.mockDbContext = new Mock<TodoContext>(options);
        }


        [Fact]
        public void TodoService_GetTodosAsync_ReturnsTodos() 
        {
            // Arrange
            var mockSet = new Mock<DbSet<Todo>>();
            mockDbContext.Setup(m => m.Todos).Returns(mockSet.Object);


            var service = new TodoService(mockDbContext.Object);


            // Act
            var actual = service.GetTodosAsync();

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Task<IEnumerable<TodoSummaryDto>>>(actual);
        }


        [Fact]
        public void TodoService_CreateTodoAsync_CreatesATodo() {
            // Arrange
            CreateTodoDto dummyTodo = new CreateTodoDto("Dummy", 1, 1, new DateOnly());
            mockDbContext.Setup(m => m.Todos.Add(It.IsAny<Todo>()));

            var service = new TodoService(mockDbContext.Object);
            // Act
            var actual = service.CreateTodoAsync(dummyTodo);

            // Assert
            mockDbContext.Verify(m => m.Todos.Add(It.IsAny<Todo>()), Times.Once); 
            Assert.NotNull(actual);
        }

        [Fact]
        public void TodoService_CreateTodoAsync_NotCreatesATodo_WhenTodoNotCorrect() {
            CreateTodoDto dummyTodo = new CreateTodoDto("", -1, -1, new DateOnly() );
            mockDbContext.Setup(m => m.Todos.Add(It.IsAny<Todo>()));

            var service = new TodoService(mockDbContext.Object);
            // Act
            var action = () => service.CreateTodoAsync(dummyTodo);

            // Assert
            mockDbContext.Verify(m => m.Todos.Add(It.IsAny<Todo>()), Times.Never);
            Assert.ThrowsAsync<ArgumentException>(action);            
        }

        [Fact]
        public void TodoService_DeleteTodoAsync_DeletesATodo()
        {
            
            var mockIQueryable = new Mock<IQueryable<Todo>>();           
            mockIQueryable.Setup(m => m.ExecuteDeleteAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            
            var mockSet = new Mock<DbSet<Todo>>(mockIQueryable);
            
            mockDbContext.Setup(m => m.Todos).Returns(mockSet.Object);

            
            var result = this.service.DeleteTodoAsync(1);

            //verify that ExecuteDeleteAsync is called at least once
            mockIQueryable.Verify(m => m.ExecuteDeleteAsync(It.IsAny<CancellationToken>()), Times.Once);



        }
    }
}
