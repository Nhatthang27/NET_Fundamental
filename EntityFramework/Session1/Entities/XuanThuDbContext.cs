﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Session1.Entities
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
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
