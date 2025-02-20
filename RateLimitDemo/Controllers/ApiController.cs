using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimitDemo.Controllers
{
    [Route("api/v1")]
    public class ApiController : Controller
    {
        private static int _counter = 0;

        [HttpGet("time")]
        [EnableRateLimiting("ConcurrencyLimiter")]
        public IActionResult CurrentTime()
        {
            _counter++;
            //Console.WriteLine($"API accessed {_counter} times.");

            var currentTime = DateTime.Now.ToString();
            return Ok(currentTime);
        }
    }
}
