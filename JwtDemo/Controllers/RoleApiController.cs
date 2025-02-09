using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    public class RoleApiController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return Json("haha");
        }
    }
}
