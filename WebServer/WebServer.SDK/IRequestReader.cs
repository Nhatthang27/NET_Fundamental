using System.Net.Sockets;

namespace WebServer.SDK
{
    public interface IRequestReader
    {
        Task<WRequest> ReadRequestAsync(CancellationToken cancellationToken);
    }
}
