using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
