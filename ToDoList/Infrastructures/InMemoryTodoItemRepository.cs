using Entities;
using UseCases;

namespace Infrastructures
{
    public class InMemoryTodoItemRepository : ITodoItemRepository
    {
        // This is a simple in-memory repository implementation
        private readonly List<TodoItem> _items = new List<TodoItem>();

        public IEnumerable<TodoItem> GetItems()
        {
            return _items;
        }

        public void Add(TodoItem item)
        {
            _items.Add(item);
        }

        public TodoItem? GetById(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void Update(TodoItem item)
        {
            var existingItem = GetById(item.Id);
            if (existingItem != null)
            {
                existingItem.Content = item.Content;
                existingItem.IsDone = item.IsDone;
            }
        }
    }
}
