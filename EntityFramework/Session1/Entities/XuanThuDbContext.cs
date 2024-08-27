using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Session1.Entities
{
    public class XuanThuDbContext : DbContext
    {
        //log sql query
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public const string _connectionString = @"Data Source=localhost,1433;Initial Catalog=XuanThuDb;uid=sa;password=sa123456;TrustServerCertificate=True";

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
