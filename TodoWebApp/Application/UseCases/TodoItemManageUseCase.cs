using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
    public class TodoItemManageUseCase
    {
        private readonly ITodoItemRepository _repository;
        public TodoItemManageUseCase(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            return await _repository.AddAsync(item);
        }
        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            return await _repository.UpdateAsync(item);
        }
        public async Task<TodoItem> DeleteAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
