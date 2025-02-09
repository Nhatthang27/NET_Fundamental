using Microsoft.AspNetCore.Mvc;

namespace OidcServer.Controllers
{
    [Route(".well-known/jwks.json")]
    public class JwksController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "OidcDiscovery", "jwks.json"), "application/json");
        }
    }
}
