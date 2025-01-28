using System.Text;

namespace WebServer.SDK;

public class StringResponseBodyWriter : IResponseBodyWriter
{
    private readonly byte[] _contentByte;

    public StringResponseBodyWriter(string content)
    {
        _contentByte = Encoding.UTF8.GetBytes(content);
    }

    public async Task WriteAsync(Stream stream)
    {
        await stream.WriteAsync(_contentByte);
    }
}