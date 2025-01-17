using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, ChatProtocol.Constants.DefaultPort);
            var clientSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );

            await clientSocket.ConnectAsync(endPoint);
            Console.WriteLine("Connected to server");
            var buffer = new byte[1024];
            var receivedBytes = await clientSocket.ReceiveAsync(buffer);
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine("Receive: {0}", receivedMessage);
            while (true)
            {
                Console.Write("Send: ");

                var message = Console.ReadLine();
                while (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("Message cannot be empty");
                    message = Console.ReadLine();
                }
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await clientSocket.SendAsync(messageBytes);
            }
        }
    }
}
