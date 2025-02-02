using ConfigurationDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace ConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigurationRoot _configuration;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            _configuration = (IConfigurationRoot)configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("provider")]
        public IActionResult Provider()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Configuation stringbuilder: ");
            foreach (var provider in _configuration.Providers)
            {
                sb.AppendLine(provider.ToString());
            }
            return Content(sb.ToString(), "text/html");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
