using System.Data.SqlClient;

namespace SqlConnectionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        static void TestConnectionString()
        {
            string sqlConnectionString = "Data Source = localhost, 1433; Initial Catalog = Northwind; User ID = sa; Password = sa123456;";
            using (SqlConnection
                    connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                connection.Close();
                Console.WriteLine(connection.State);
            }
        }

        static void TestConnectionStringBuilder()
        {
            var DbCStringBuilder = new SqlConnectionStringBuilder();
            DbCStringBuilder["Server"] = "127.0.0.1,1433";
            DbCStringBuilder["Database"] = "Northwind";
            DbCStringBuilder["User Id"] = "sa";
            DbCStringBuilder["Password"] = "sa123456";

            using (SqlConnection connection = new SqlConnection(DbCStringBuilder.ConnectionString))
            {
                connection.Open();
                connection.Close();
                Console.WriteLine(connection.State);
            }
        }
    }
}
