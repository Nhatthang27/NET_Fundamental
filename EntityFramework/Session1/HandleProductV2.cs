using Microsoft.EntityFrameworkCore;
using Session1.Entities;

namespace Session1
{
    internal class HandleProductV2
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

        static void DropDatabase()
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

        static void InsertCategory()
        {
            using var context = new XuanThuDbContext();
            Category c1 = new Category() { Name = "Phone", Description = "Samsung, Iphone, ..." };
            Category c2 = new Category() { Name = "Fruit", Description = "Orange, Lemon, ..." };

            context.Categories.Add(c1);
            context.Categories.Add(c2);
            context.SaveChanges();
        }

        static void InsertProduct()
        {
            using var context = new XuanThuDbContext();
            var c1 = (from c in context.Categories where c.CategoryId == 1 select c).FirstOrDefault();
            var c2 = (from c in context.Categories where c.CategoryId == 2 select c).FirstOrDefault();

            Product p1 = new() { Name = "Iphone 17", Price = 180000, Stock = 10, CategoryId = 1 };
            Product p2 = new() { Name = "Samsung s23 ultra", Price = 40000, Stock = 100, CategoryId = 1 };
            Product p3 = new() { Name = "Lipovitan", Price = 100, Stock = 11100, Category = c2 };
            Product p4 = new() { Name = "Oppo Neo 7", Price = 900, Stock = 900, Category = c1 };


            context.Products.Add(p1);
            context.Products.Add(p2);
            context.Products.Add(p3);
            context.Products.Add(p4);
            context.SaveChanges();
        }


        static void RetrieveProduct()
        {
            using var context = new XuanThuDbContext();
            var p2 = (from p in context.Products where p.Id == 2 select p).FirstOrDefault();
            var p3 = (from p in context.Products where p.Id == 3 select p).FirstOrDefault();

            context.Entry(p2).Reference(p => p.Category).Load(); //ef không tự động gán Navigation Object, reference phải tự gán
            Console.WriteLine(p2);
            Console.WriteLine(p3);
        }

        static void RetrieveCategory()
        {
            using var context = new XuanThuDbContext();
            //var c1 = (from p in context.Products where p.CategoryId == 1 select p).ToList();
            var c1_v2 = context.Categories.Find(1);

            context.Entry(c1_v2).Collection(c => c.Products).Load();
            if (c1_v2.Products != null)
                foreach (var p in c1_v2.Products.ToList())
                {
                    Console.WriteLine(p);
                }
            else
                Console.WriteLine("Null");
        }

        static void QueryWithLinq()
        {
            using var context = new XuanThuDbContext();
            var products = (from p in context.Products
                            join c in context.Categories on p.CategoryId equals c.CategoryId
                            select new { Name = p.Name, Price = p.Price, CategoryName = c.Name });
            products.ToList().ForEach(p => Console.WriteLine($"{p.Name}, {p.Price}, {p.CategoryName}"));
        }
        public static void Main(string[] args)
        {
            //DropDatabase();
            //CreateDatabase();
            //InsertCategory();
            //InsertProduct();
            QueryWithLinq();
        }
    }
}
