using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
