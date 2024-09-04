using System.Net;

namespace Networking
{
    internal class Test1
    {
        static void GetInfo()
        {
            string url = "https://github.com/dotnet/vscode-csharp/releases";
            var uri = new Uri(url);
            var uritype = typeof(Uri);
            uritype.GetProperties().ToList().ForEach(property =>
            {
                Console.WriteLine($"{property.Name,15} {property.GetValue(uri)}");
            });
            Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");
        }

        static void GetHostName()
        {
            var hostname = Dns.GetHostName();
            Console.WriteLine($"Host Name: {hostname}");

            string url = "https://www.facebook.com/";
            var uri = new Uri(url);
            var hostEntry = Dns.GetHostEntry(uri.Host);
            Console.WriteLine($"Host {uri.Host} có các IP");
            hostEntry.AddressList.ToList().ForEach(ip => Console.WriteLine(ip));
        }
        static void Main(string[] args)
        {
            GetHostName();

        }
    }
}
