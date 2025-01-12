using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    [Route("response-demo")]
    public class ResponseDemoController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
