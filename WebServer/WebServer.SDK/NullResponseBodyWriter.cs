using System;

namespace WebServer.SDK;

public class NullResponseBodyWriter : IResponseBodyWriter
{
    public Task WriteAsync(Stream stream)
    {
        return Task.CompletedTask;
    }

    public static NullResponseBodyWriter Instance { get; } = new NullResponseBodyWriter();
}
