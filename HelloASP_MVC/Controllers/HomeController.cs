using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelloASP_MVC.Models;

namespace HelloASP_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository _repository;

    public HomeController(IRepository repository, ILogger<HomeController> logger)
    {
        _logger = logger;
        _logger.LogInformation("new controller");
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View(new HelloModel { Name = "World" });
    }

    public IActionResult Privacy()
    {
        return View("Index", new HelloModel { Name = "Privacy" });
    }

    [NonAction]
    public IActionResult Contract()
    {
        return View();
    }

    public IActionResult NewActionMethod(string name)
    {
        return Content("This is a new action method: " + _repository.GetId(name));
    }

    [HttpGet]
    [Route("add-user")]
    public IActionResult AddUser([FromForm] string param)
    {
        _logger.LogInformation("[AddUser] METHOD {M}, param = {P} ", Request.Method, param);
        return Content("Hello " + param);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
