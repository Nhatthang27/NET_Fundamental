using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IdentityModelEventSource.ShowPII = true;
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                                options =>
                                {
                                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                    {
                                        ValidateIssuer = false,
                                        ValidateAudience = false,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = false,
                                        RequireExpirationTime = true,
                                        ValidIssuer = "https://mysso-server.com",
                                        ValidAudience = "https://localhost:5000",
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345270202!!!nfguyentrihfcatlinh&&&****sjjjjsppelsaer00991298847382"))
                                    };
                                    options.Events = new JwtBearerEvents
                                    {
                                        OnAuthenticationFailed = context =>
                                        {
                                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                            {
                                                context.Response.Headers.TryAdd("Token-Expired", "true");
                                            }
                                            return Task.CompletedTask;
                                        }
                                    };
                                });

            builder.Services.AddAuthorization(options =>
            {
                //OR
                options.AddPolicy("policy-admin", policy => policy.RequireRole("admin", "manager"));
                //AND
                options.AddPolicy("policy-main", policy => policy.RequireRole("admin").RequireRole("manager"));
                options.AddPolicy("policy-user", policy => policy.RequireRole("user"));
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
