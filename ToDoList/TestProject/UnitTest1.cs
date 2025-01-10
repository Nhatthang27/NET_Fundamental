using Entities;
using Infrastructures;
using UseCases;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void CreateTodoItemAndMarkDone()
        {
            // Arrange
            var mockRepository = new InMemoryTodoItemRepository();
            var todoListManager = new TodoListManager(mockRepository);

            var todoItem = new TodoItem
            {
                Id = 1,
                Content = "Sample Todo Item",
                IsDone = false
            };

            // Act
            todoListManager.AddTodoItem(todoItem);
            todoListManager.MaskAsDone(todoItem.Id);

            // Assert
            Assert.True(todoListManager.GetTodoItems().First().IsDone);
            Assert.Equal("Sample Todo Item", todoListManager.GetTodoItems().First().Content);
        }
    }
}