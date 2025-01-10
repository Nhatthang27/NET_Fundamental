using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace TodoList.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoListManager _todoListManager;

        public HomeController(TodoListManager todoListManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _todoListManager = todoListManager;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var items = _todoListManager.GetTodoItems();
            return View(new TodoListViewModel { Items = items });
        }

        [HttpPost]
        [Route("add-item")]
        public IActionResult AddItem([FromForm] string content)
        {
            _logger.LogInformation("AddItem: {Content}", content);
            _todoListManager.AddTodoItem(new TodoItem { Content = content, Id = _todoListManager.GetTodoItems().Count() + 1 });
            return RedirectToAction("Index");
        }

        // [HttpPatch]
        // [Route("/mask-done-item/{id}")]
        // public IActionResult MarkDoneItem([FromRoute] int id, [FromBody] JsonPatchDocument<TodoItem> patchDocument)
        // {
        //     if (patchDocument == null)
        //     {
        //         return BadRequest("Invalid patch document");
        //     }
        //     _logger.LogInformation("MarkDoneItem: {Id}", id);
        //     var item = _todoListManager.GetItemById(id);
        //     if (item == null)
        //     {
        //         return NotFound($"Item with id {id} not found");
        //     }
        //     patchDocument.ApplyTo(item, ModelState);
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     return View("Index");
        // }

        [HttpPost]
        [Route("mark-done")]
        public IActionResult MarkDoneItem([FromForm] int id)
        {
            _logger.LogInformation("MarkDoneItem: {Id}", id);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                _todoListManager.MaskAsDone(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking item as done: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
