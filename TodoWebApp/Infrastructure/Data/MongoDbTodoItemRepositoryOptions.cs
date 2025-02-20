namespace Infrastructure.Data
{
    public class MongoDbTodoItemRepositoryOptions
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
