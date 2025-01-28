using System.Net.Sockets;

namespace WebServer.SDK
{
    public interface IRequestReaderFactory
    {
        IRequestReader Create(Socket socket);
    }
}
