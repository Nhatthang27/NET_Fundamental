using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Auth.WebApi.Custom.JWT.Auth
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IConfiguration configuration)
            : base(options, logger, encoder)
        {
            _configuration = configuration;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_configuration.GetSection("ApiKeySettings:ApiKeyName").Value!, out var apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail("API Key is missing"));
            }

            var requiredApiKeyEndpoints = _configuration.GetSection("ApiKeySettings:Endpoints").Get<Dictionary<string, List<string>>>();

            if (!requiredApiKeyEndpoints.TryGetValue(Context.Request.Path, out var validApiKeys))
            {
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity()), "ApiKeyAuth")));
            }

            if (!validApiKeys.Contains(apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
            var identity = new ClaimsIdentity(claims, "ApiKeyAuth");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "ApiKeyAuth");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
