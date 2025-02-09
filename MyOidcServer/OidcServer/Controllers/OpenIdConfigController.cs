using Microsoft.AspNetCore.Mvc;

namespace OidcServer.Controllers
{
    [Route(".well-known/openid-configuration")]
    public class OpenIdConfigController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "OidcDiscovery", "openid-configuration.json"), "application/json");
        }
    }
}
