using Microsoft.AspNetCore.Mvc;
using MiddlewareDemo.Models;
namespace MiddlewareDemo.Controllers
{
    public class ClientInfoController : Controller
    {
        [Route("/clientinfo")]
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            return Ok(HttpContext.Features.Get<ClientInfo>());
        }
    }
}
