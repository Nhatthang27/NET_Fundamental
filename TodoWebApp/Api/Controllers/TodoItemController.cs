using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/to-do-item")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoItemManageUseCase _todoItemManageUseCase;

        public TodoItemController(TodoItemManageUseCase todoItemManageUseCase)
        {
            _todoItemManageUseCase = todoItemManageUseCase;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemManageUseCase.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _todoItemManageUseCase.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var newTodoItem = await _todoItemManageUseCase.AddAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = newTodoItem.Id }, newTodoItem);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            await _todoItemManageUseCase.UpdateAsync(todoItem);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoItemManageUseCase.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoItemManageUseCase.DeleteAsync(id);
            return NoContent();
        }

    }
}
