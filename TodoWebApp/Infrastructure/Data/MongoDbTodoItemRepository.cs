using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class MongoDbTodoItemRepository : ITodoItemRepository
    {
        private readonly IMongoCollection<TodoItem> _collection;
        public MongoDbTodoItemRepository(MongoDbTodoItemRepositoryOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);
            _collection = database.GetCollection<TodoItem>("TodoItems");
        }
        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _collection.Find(item => true).ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        public Task<TodoItem> UpdateAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
        public Task<TodoItem> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
