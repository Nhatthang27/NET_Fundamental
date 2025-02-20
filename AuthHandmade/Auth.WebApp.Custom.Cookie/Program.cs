using Auth.WebApp.Custom.Cookie.BLL;
using Auth.WebApp.Custom.Cookie.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
namespace Auth.WebApp.Custom.Cookie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddDbContext<CustomCookieDbContext>(options =>
                  options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
                builder.Services.AddScoped<AccountService>();
                // Add services to the container.
                builder.Services.AddControllersWithViews();

                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login"; // tự động gọi đến nếu truy cập vào api authorize
                        options.LogoutPath = "/Account/Logout";
                        options.AccessDeniedPath = "/Account/AccessDenied";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.SlidingExpiration = true;
                    });
                builder.Services.AddAuthorization();

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

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
        }
    }
}