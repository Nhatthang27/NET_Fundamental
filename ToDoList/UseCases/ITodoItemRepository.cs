using Entities;

namespace UseCases
{
    public interface ITodoItemRepository
    {
        void Add(TodoItem item);
        void Delete(int id);
        void Update(TodoItem item);
        TodoItem? GetById(int id);
        IEnumerable<TodoItem> GetItems();
    }
}