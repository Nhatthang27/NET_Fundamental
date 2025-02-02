using Microsoft.AspNetCore.Mvc;

namespace SessionDemo.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetSession(string key, string value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HttpContext.Session.SetString(key, value);
            return Ok($"Session: Key[{key}] = Value[{value}]");
        }

        public IActionResult GetSession(string key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(HttpContext.Session.GetString(key) ?? "No value found for the key");
        }
    }
}
