using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class FunctionController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Valami----------");
            return View();
        }
    }
}
