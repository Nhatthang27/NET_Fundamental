
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Session1.Entities;

namespace Session1
{
    internal class Program
    {
        static void CreateDatabase()
        {

            using var context = new XuanThuDbContext();
            string dbname = context.Database.GetDbConnection().Database;
            bool result = context.Database.EnsureCreated();
            if (result)
            {
                Console.WriteLine($"Database {dbname} is created");
            }
            else
            {
                Console.WriteLine($"Database {dbname} is already existed");
            }
        }

        public static void DropDatabase()
        {
            using var context = new XuanThuDbContext();
            string dbname = context.Database.GetDbConnection().Database;
            bool result = context.Database.EnsureDeleted();
            if (result)
            {
                Console.WriteLine($"Database {dbname} is deleted");
            }
            else
            {
                Console.WriteLine($"Database {dbname} is not existed");
            }
        }

        public static void InsertProduct()
        {
            using var context = new XuanThuDbContext();
            var product = new Product
            {
                Name = "Laptop",
                Price = 1000,
                Stock = 10
            };
            context.Products.Add(product);
            context.SaveChanges();
        }

        public static void InsertRange()
        {
            using var context = new XuanThuDbContext();
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Price = 1000,
                    Stock = 10
                },
                new Product
                {
                    Name = "Mouse",
                    Price = 10,
                    Stock = 100
                }
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void ReadProduct()
        {
            using var context = new XuanThuDbContext();

            var products = context.Products.ToList();
            //linq
            products.ForEach(product => Console.WriteLine(product));

            products.ForEach(delegate (Product product) { Console.WriteLine(product.Name); });

            //sql
            var products2 = context.Products.FromSqlRaw("SELECT * FROM Product").ToList();
            var products3 = from p in context.Products
                            where p.Price > 100
                            select p;
        }

        public static void UpdateProduct()
        {
            using var context = new XuanThuDbContext();
            var product = context.Products.Find(1);
            if (product != null)
            {
                product.Price = 2000;
                context.SaveChanges();
            }
        }

        //tách sự theo dõi ra khỏi DB
        public static void RenameProduct()
        {
            using var context = new XuanThuDbContext();
            Product product = (from p in context.Products
                               where p.Id == 1
                               select p).FirstOrDefault();
            if (product != null)
            {
                //đối tượng theo dõi table trên db
                EntityEntry<Product> entry = context.Entry(product);
                entry.State = EntityState.Detached;
                product.Name = "Smartphone";
                context.Products.Update(product);
                context.SaveChanges();
            }
        }

        public static void Logging()
        {
            //entity framework không code sql mà dùng linq để lấy data
            //logging sẽ giúp chúng ta in ra câu lệnh sql mà entity framework thực thi
        }
        static void Main(string[] args)
        {
            //CreateDatabase();
            //DropDatabase();
            //InsertProduct();
            InsertRange();
        }
    }
}
