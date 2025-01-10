using Entities;
namespace UseCases
{
    public class TodoListManager(ITodoItemRepository todoListRepository)
    {
        public IEnumerable<TodoItem> GetTodoItems()
        {
            return todoListRepository.GetItems();
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            todoListRepository.Add(todoItem);
        }

        public void MaskAsDone(int id)
        {
            var item = todoListRepository.GetById(id);
            if (item == null)
            {
                throw new ArgumentException("Item not found");
            }
            else
            {
                item.IsDone ^= true;
            }
        }

        public void DeleteItem(int id)
        {
            todoListRepository.Delete(id);
        }

        public TodoItem? GetItemById(int id)
        {
            return todoListRepository.GetById(id);
        }

        public void UpdateItem(TodoItem item)
        {
            var existingItem = todoListRepository.GetById(item.Id);
            if (existingItem == null)
            {
                throw new ArgumentException("Item not found");
            }
            existingItem.Content = item.Content;
            existingItem.IsDone = item.IsDone;
        }
    }
}
