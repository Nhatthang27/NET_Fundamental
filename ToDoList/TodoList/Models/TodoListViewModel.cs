namespace TodoList.Models
{
    public class TodoListViewModel
    {
        public required IEnumerable<TodoItem> Items { get; init; }
    }
}
