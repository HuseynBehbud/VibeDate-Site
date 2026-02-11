using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
