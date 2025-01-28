using WebServer.SDK;

namespace WebServer.Server.Middleware
{
    internal class NotFoundMiddleware : IMiddleware
    {
        private static readonly IResponseBodyWriter EmptyBodyContentWriter = new StringResponseBodyWriter("");

        public Task InvokeAsync(MiddlewareContext context, IMiddleware next, CancellationToken cancellationToken)
        {
            context.Response.ContentLength = 0;
            context.Response.HttpResponseCodes = HttpResponseCodes.NotFound;
            context.Response.ContentType = "text/html";
            context.Response.BodyWriter = EmptyBodyContentWriter;

            return Task.CompletedTask;
        }
    }
}
