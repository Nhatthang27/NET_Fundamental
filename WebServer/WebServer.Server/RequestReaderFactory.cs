using System.Net.Sockets;
using WebServer.SDK;
using WebServer.Server.RequestReaders;

namespace WebServer.Server
{
    internal class RequestReaderFactory : IRequestReaderFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        public RequestReaderFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IRequestReader Create(Socket socket)
        {
            return new DefaultRequestReader(socket, _loggerFactory.CreateLogger<DefaultRequestReader>());
        }
    }
}
