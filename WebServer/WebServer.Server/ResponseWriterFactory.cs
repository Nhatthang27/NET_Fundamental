using System.Net.Sockets;
using WebServer.SDK;
using WebServer.Server.ResponseWriters;

namespace WebServer.Server;

public class ResponseWriterFactory : IResponseWriterFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public ResponseWriterFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public IResponseWriter Create(Socket socket)
    {
        return new DefaultResponseWriter(socket, _loggerFactory.CreateLogger<DefaultResponseWriter>());
    }
}
