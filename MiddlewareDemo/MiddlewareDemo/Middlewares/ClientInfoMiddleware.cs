using MiddlewareDemo.ClientInfoRepository;

namespace MiddlewareDemo.Middlewares
{
    public class ClientInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientInfoMiddleware> _logger;
        private readonly IClientInfoRepositoryFactory _clientRepositoryFactory;

        public ClientInfoMiddleware(RequestDelegate next, ILogger<ClientInfoMiddleware> logger, IClientInfoRepositoryFactory clientRepositoryFactory)
        {
            _next = next;
            _logger = logger;
            _logger.LogInformation("ClientInfoMiddleware is executing.");
            _clientRepositoryFactory = clientRepositoryFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("ClientInfoMiddleware invoke.");
            var apiKey = context.Request.Headers["API-KEY"].FirstOrDefault();
            if (!string.IsNullOrEmpty(apiKey))
            {
                var clientRepository = _clientRepositoryFactory.Create();
                var clientInfo = clientRepository.GetClientInfo(apiKey);
                if (clientInfo != null)
                {
                    context.Features.Set(clientInfo);
                    await _next(context);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
        }
    }
}