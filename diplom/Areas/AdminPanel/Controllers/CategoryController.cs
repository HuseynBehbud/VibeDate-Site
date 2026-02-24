using diplom.Areas.AdminPanel.ViewModels;
using diplom.Data;
using diplom.Models;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private VibeDateDbContext _context { get; }
        public CategoryController(VibeDateDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View(_context.categories.Where(c => !c.isDeleted));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if(_context.categories.Any(c => c.Name == categoryViewModel.Name))
            {
                ModelState.AddModelError("Name", "Bu adda kategoriya mövcuddur");
                return View();
            }
            Category category = new Category()
            {
                Name = categoryViewModel.Name
            };
            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
