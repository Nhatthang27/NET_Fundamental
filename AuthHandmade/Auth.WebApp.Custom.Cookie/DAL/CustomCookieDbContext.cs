using Auth.WebApp.Custom.Cookie.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.WebApp.Custom.Cookie.DAL
{
    public class CustomCookieDbContext : DbContext
    {
        public CustomCookieDbContext(DbContextOptions<CustomCookieDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
