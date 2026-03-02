using diplom.Data;
using diplom.Models;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class HomeController : Controller
    {
        private VibeDateDbContext _context { get; }
        public HomeController(VibeDateDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult DateIdeas()
        {
            List<Products> products = _context.products.ToList();
            return View(products);
        }
        public IActionResult DatePlan()
        {
            return View();
        }
        public IActionResult FirstDate()
        {
            return View();
        }
        public IActionResult ForgotPass()
        {
            return View();
        }
       
        
        public IActionResult LogAbout()
        {
            return View();
        }
        public IActionResult LogBlog()
        {
            return View();
        }
        public IActionResult LogDatePlan()
        {
            return View();
        }
        public IActionResult LogFirstDate()
        {
            return View();
        }
        public IActionResult LogIndex()
        {
            return View();
        }
        public IActionResult LogPay()
        {
            return View();
        }
        public IActionResult LogPLan()
        {
            return View();
        }
        public IActionResult Pay()
        {
            return View();
        }
        public IActionResult Payments()
        {
            return View();
        }
        public IActionResult Plan()
        {
            return View();
        }
        public IActionResult PlanADate()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult Subscriptions()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
