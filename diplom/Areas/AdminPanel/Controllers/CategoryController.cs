using diplom.Areas.AdminPanel.ViewModels;
using diplom.Data;
using diplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.categories.Any(c => c.Name == categoryViewModel.Name))
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
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category existCategory = _context.categories.Where(c => !c.isDeleted).FirstOrDefault(c => c.Id == id);
            if (existCategory == null)
            {
                return NotFound();
            }
            return View(existCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id != category.Id) return BadRequest();
            if (!ModelState.IsValid) return View(category);
            Category existCategory = await _context.categories.FindAsync(id);
            if (existCategory == null) return NotFound();
            if (_context.categories.Any(c => c.Name == category.Name && c.Id != id))
            {
                ModelState.AddModelError("Name", "Bu adda kategoriya artıq mövcuddur.");
                return View(category);
            }
            existCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            Category existCategory = _context.categories.Where(c => !c.isDeleted).FirstOrDefault(c => c.Id == id);
            if (existCategory == null)
            {
                return NotFound();
            }
            //_context.Remove(existCategory);
            existCategory.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
    }
}
