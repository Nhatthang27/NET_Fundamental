using Domain.Entities;
using Infrastructure.Data;

namespace RepositoryTest
{
    public class MongoDbToDoItemRepositoryTests
    {

        [Fact]
        public async Task AddAsync()
        {
            // Arrange
            var repository = new MongoDbTodoItemRepository(
                new MongoDbTodoItemRepositoryOptions
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TodoApiDbTest"
                }
                );
            var item = new TodoItem { Id = new Random().NextInt64(1, Int64.MaxValue), Name = "Test Item", IsComplete = false };
            // Act
            var result = await repository.AddAsync(item);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(item.Id, result.Id);
            Assert.Equal(item.Name, result.Name);
            Assert.Equal(item.IsComplete, result.IsComplete);

            var fromDb = await repository.GetByIdAsync(item.Id);
            Assert.NotNull(fromDb);
            Assert.Equal(item.Id, fromDb.Id);
            Assert.Equal(item.Name, fromDb.Name);
            Assert.Equal(item.IsComplete, fromDb.IsComplete);
        }
    }
}
