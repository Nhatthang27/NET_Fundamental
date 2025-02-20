using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApi.Custom.JWT.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new { Username = User.Identity.Name, Role = User.FindFirst("role")?.Value });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdminData()
        {
            return Ok(new { message = "Welcome, Admin!" });
        }
    }
}
