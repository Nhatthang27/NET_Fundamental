using MySession.MySession;
using System.Reflection;
public class Program
{
    private static readonly ILogger<Program> _logger;

    static Program()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _logger = loggerFactory.CreateLogger<Program>();
    }

    public static void Main(string[] args)
    {
        var type = Assembly.GetExecutingAssembly().GetType("Program");
        _logger.LogInformation(type?.Namespace ?? "No Namespace");

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IMySessionStorageEngine>(services =>
        {
            var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
            Directory.CreateDirectory(path);

            return new FileMySessionStorageEngine(path);
        });

        builder.Services.AddSingleton<IMySessionStorage, MySessionStorage>();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSession();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
