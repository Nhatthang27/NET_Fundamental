using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    public class PbApiController : Controller
    {
        public IActionResult Index()
        {
            return Json("Policy-based");
        }
    }
}
