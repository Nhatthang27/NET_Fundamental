using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetByIdAsync(long id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> AddAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);
        Task<TodoItem> DeleteAsync(long id);
    }
}
