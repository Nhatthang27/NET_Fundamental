using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluentAPI.Entities
{
    public class XuanThuDbContext : DbContext
    {
        //log sql query
        //log các sql query từ linq ra terminal
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
               .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information) // Ghi nhật ký thông tin truy vấn
               .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information) // Ghi nhật ký thông tin truy vấn
               .AddConsole();
        }
        );
        public const string _connectionString = @"Data Source=localhost,1433;Initial Catalog=XuanThuDb;uid=sa;password=sa123456;TrustServerCertificate=True";

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Category).WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Product_Category");
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
