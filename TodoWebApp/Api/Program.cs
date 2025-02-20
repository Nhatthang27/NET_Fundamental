using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Data;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton(new MongoDbTodoItemRepositoryOptions
            {
                ConnectionString = builder.Configuration.GetConnectionString("TodoApiDatabase")!,
                DatabaseName = builder.Configuration["TodoApiDatabaseName"]!
            });
            builder.Services.AddTransient<ITodoItemRepository, MongoDbTodoItemRepository>();
            builder.Services.AddScoped<TodoItemManageUseCase>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
