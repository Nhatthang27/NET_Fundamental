using System.Net;

namespace Auth.WebApi.Custom.JWT.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            // if user is authenticated, skip this middleware
            if (context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            // get list of endpoints that require API key
            var requiredApiKeyEndpoints = _configuration.GetSection("ApiKeySettings:Endpoints").Get<Dictionary<string, List<string>>>();

            string reqEnpoint = context.Request.Path.Value;
            if (!requiredApiKeyEndpoints.ContainsKey(reqEnpoint))
            {
                await _next(context);
                return;
            }

            string apiKeyName = _configuration.GetSection("ApiKeySettings:ApiKeyName").Value;
            
            if (!context.Request.Headers.TryGetValue(apiKeyName, out var apiKeyReq))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Api Key is missing");
                return;
            }

            if (!requiredApiKeyEndpoints[reqEnpoint].Contains(apiKeyReq))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Invalid Api Key");
                return;
            }
            await _next(context);
        }
    }
}
