using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApi.Custom.JWT.Controllers
{
    [Authorize(Policy = "JwtOrApiKey")]
    [Route("api/third-party-apikey")]
    [ApiController]
    public class ThirdPartyApiKeyController : Controller
    {
        [HttpGet("kill")]
        public IActionResult Kill()
        {
            return Ok("Access successfully");
        }
    }
}
