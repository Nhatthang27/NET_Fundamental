using System.Net;
using System.Net.Sockets;
using WebServer.SDK;
using WebServer.Server.DefaultMiddlewares;
using WebServer.Server.Middleware;

namespace WebServer.Server;

public class Worker : BackgroundService
{
    private readonly WebServerOptions _options;
    private readonly ILogger<Worker> _logger;
    private readonly IRequestReaderFactory _requestReaderFactory;
    private readonly IResponseWriterFactory _responseWriterFactory;
    private readonly Tuple<IMiddleware, IMiddleware> _middleware = new(new NotFoundMiddleware(), NullMiddleware.Instance);
    public Worker(WebServerOptions options, ILogger<Worker> logger, IRequestReaderFactory requestReaderFactory,
                    IResponseWriterFactory responseWriterFactory)
    {
        _options = options;
        _logger = logger;
        _requestReaderFactory = requestReaderFactory;
        _responseWriterFactory = responseWriterFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var endPoint = new IPEndPoint(string.IsNullOrEmpty(_options.IPAddress) ? IPAddress.Any : IPAddress.Parse(_options.IPAddress), _options.Port);
        using var serverSocket = new Socket(
            endPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
            );
        serverSocket.Bind(endPoint);
        _logger.LogInformation("Listening... (port: {P})", _options.Port);
        serverSocket.Listen();

        var clientConnections = new List<ClientConnection>();
        while (!stoppingToken.IsCancellationRequested)
        {
            var clientSocket = await serverSocket.AcceptAsync(stoppingToken);

            if (clientSocket != null)
            {
                var t = HandleClientConnectionAsync(clientSocket, stoppingToken);
                clientConnections.Add(new ClientConnection
                {
                    HandlerTask = t
                });
            }
        }
        await Task.WhenAll(clientConnections.Select(s => s.HandlerTask).ToArray());
    }

    private async Task HandleClientConnectionAsync(Socket clientSocket, CancellationToken stoppingToken)
    {
        try
        {
            var cancelationTokenSource = new CancellationTokenSource(3000);

            // read request from socket
            IRequestReader requestReader = _requestReaderFactory.Create(clientSocket);
            WRequest request = await requestReader.ReadRequestAsync(CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, cancelationTokenSource.Token).Token);

            // handle request
            var content = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>Web Server Response</title>
        <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; }}
        .container {{ max-width: 800px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; }}
        h1 {{ color: #333; text-align: center; }}
        .info {{ background-color: #f0f0f0; padding: 15px; border-radius: 5px; }}
        .timestamp {{ color: #666; font-size: 0.9em; text-align: right; }}
        </style>
    </head>
    <body>
        <div class='container'>
        <h1>Hello from C# Web Server</h1>
        <div class='info'>
            <p>Server Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
            <p>Request Path: {request.Url}</p>
        </div>
        <p class='timestamp'>Generated at: {DateTime.UtcNow:R}</p>
        </div>
    </body>
    </html>";
            WResponse response = new WResponse()
            {
                // HttpVersion = request.HttpVersion,
                // HttpResponseCodes = HttpResponseCodes.OK,
                // ContentLength = content.Length,
                // ContentType = "text/html",
                // BodyWriter = new StringResponseBodyWriter(content)
            };
            var invokeCancelationTokenSource = new CancellationTokenSource(15000);
            await InvokeMiddlewareAsync(
                new MiddlewareContext
                {
                    Request = request,
                    Response = response
                },
                CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, invokeCancelationTokenSource.Token).Token
            );


            // send back the response
            IResponseWriter responseWriter = _responseWriterFactory.Create(clientSocket);
            await responseWriter.SendResponseAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling client connection.");
        }
        finally
        {
            clientSocket.Close(); // Đóng kết nối
            clientSocket.Dispose();
        }
    }

    private async Task InvokeMiddlewareAsync(MiddlewareContext context, CancellationToken stoppingToken)
    {
        await _middleware.Item1.InvokeAsync(context, _middleware.Item1, stoppingToken);
    }
}
