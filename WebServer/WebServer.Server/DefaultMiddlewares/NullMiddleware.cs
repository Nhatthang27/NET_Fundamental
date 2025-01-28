using System;
using WebServer.SDK;

namespace WebServer.Server.DefaultMiddlewares;

public class NullMiddleware : IMiddleware
{
    public Task InvokeAsync(MiddlewareContext context, IMiddleware next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public static NullMiddleware Instance { get; } = new();

}
