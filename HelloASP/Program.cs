namespace HelloASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World hehehehehihoohoafasfasfasdfasdfasdfhohohho!");

            app.MapGet("/hello", async context =>
            {
                await context.Response.WriteAsync("Hello from MapGet");
            });


            // Map route "/abc" để trả về nội dung từ ABC
            app.Map("/abc", app1 =>
            {
                app1.Run(async context =>
                {
                    await context.Response.WriteAsync("Noi dung tra ve tu ABC");
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Terminal Middleware");
            });

            app.Run();
        }
    }
}
