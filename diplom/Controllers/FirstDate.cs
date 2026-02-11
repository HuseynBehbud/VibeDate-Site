using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class FirstDate : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
