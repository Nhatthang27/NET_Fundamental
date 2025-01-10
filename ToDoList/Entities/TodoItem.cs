namespace Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsDone { get; set; } = false;
    }
}
