using System.Net.Sockets;
using WebServer.SDK;

namespace WebServer.Server.ResponseWriters
{
    public class DefaultResponseWriter : IResponseWriter
    {
        private readonly Socket _socket;
        private readonly ILogger<DefaultResponseWriter> _logger;

        public DefaultResponseWriter(Socket socket, ILogger<DefaultResponseWriter> logger)
        {
            _socket = socket;
            _logger = logger;
        }

        public async Task SendResponseAsync(WResponse response)
        {
            var stream = new NetworkStream(_socket);
            var streamWriter = new StreamWriter(stream);

            if (string.IsNullOrEmpty(response.HttpResponsePhrases))
            {
                response.HttpResponsePhrases = HttpResponsePhrases.GetPhrase(response.HttpResponseCodes);
            }

            await streamWriter.WriteLineAsync($"{response.HttpVersion} {(int)response.HttpResponseCodes} {response.HttpResponsePhrases}");
            await streamWriter.WriteLineAsync($"Content-Type: {response.ContentType}");
            await streamWriter.WriteLineAsync($"Content-Length: {response.ContentLength}");
            await streamWriter.WriteLineAsync();
            await streamWriter.FlushAsync();
            await response.BodyWriter.WriteAsync(stream);
            await streamWriter.FlushAsync();
            _logger.LogWarning("Response sent to {A}", _socket.RemoteEndPoint);
        }
    }
}
