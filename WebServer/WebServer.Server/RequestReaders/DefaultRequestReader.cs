using System.Net.Sockets;
using System.Text;
using WebServer.SDK;

namespace WebServer.Server.RequestReaders
{
    internal class DefaultRequestReader : IRequestReader
    {
        private readonly Socket _socket;
        private readonly ILogger<DefaultRequestReader> _logger;

        public DefaultRequestReader(Socket socket, ILogger<DefaultRequestReader> logger)
        {
            _logger = logger;
            _socket = socket;
        }

        public async Task<WRequest> ReadRequestAsync(CancellationToken cancellationToken)
        {
            NetworkStream stream = new NetworkStream(_socket);
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            string? requestLineString = await reader.ReadLineAsync(cancellationToken);
            // log the address of the client
            _logger.LogWarning("Client Address: {A}", _socket.RemoteEndPoint);

            var requestBuilder = new WRequestBuilder();

            if (requestLineString != null && RequestLineParser.TryParse(requestLineString, out RequestLine? requestLine))
            {
                requestBuilder.Method = requestLine!.Method;
                requestBuilder.Url = requestLine!.Url;
                requestBuilder.HttpVersion = requestLine!.HttpVersion;

                var headerLineString = await reader.ReadLineAsync(cancellationToken);
                while (!string.IsNullOrEmpty(headerLineString))
                {
                    if (HeaderParser.TryParse(headerLineString, out WHeader? header))
                    {
                        requestBuilder.AddHeader(header!);
                    }
                    headerLineString = await reader.ReadLineAsync(cancellationToken);
                }
            }
            return requestBuilder.Build();
        }
    }
}
