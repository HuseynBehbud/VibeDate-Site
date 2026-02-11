using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class ForgotPass : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
