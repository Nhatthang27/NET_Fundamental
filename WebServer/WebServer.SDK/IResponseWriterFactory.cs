using System;
using System.Net.Sockets;

namespace WebServer.SDK;

public interface IResponseWriterFactory
{
    IResponseWriter Create(Socket socket);
}
