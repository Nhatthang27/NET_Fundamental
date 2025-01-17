using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int clientId = 0;
            //what is loopback
            //loopback is a virtual network interface that your computer uses to communicate with itself
            var endPoint = new IPEndPoint(IPAddress.Loopback, ChatProtocol.Constants.DefaultPort);
            var serverSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );

            serverSocket.Bind(endPoint);
            Console.WriteLine($"Listening to port {ChatProtocol.Constants.DefaultPort}");

            serverSocket.Listen();
            var clientHandler = new List<Task>();
            while (true)
            {
                var clientSocket = await serverSocket.AcceptAsync();
                clientHandler.Add(HandleClientRequest(clientSocket, clientId++));
            }
            Task.WaitAll([.. clientHandler]);
        }

        private static async Task HandleClientRequest(Socket clientSocket, int clientId)
        {
            var welComeBytes = Encoding.UTF8.GetBytes(ChatProtocol.Constants.WelcomeMessage);
            await clientSocket.SendAsync(welComeBytes);

            var buffer = new byte[1024];

            while (true)
            {
                var r = await clientSocket.ReceiveAsync(buffer);
                var msg = Encoding.UTF8.GetString(buffer, 0, r);

                if (string.IsNullOrEmpty(msg))
                {
                    CloseConnection(clientSocket);
                    Console.WriteLine($"[Client {clientId}] disconnected!");
                    break;
                }
                Console.WriteLine($"[Client {clientId}]: {msg}");
            }
        }

        private static void CloseConnection(Socket clientSocket)
        {
            clientSocket.Close();
        }
    }
}
