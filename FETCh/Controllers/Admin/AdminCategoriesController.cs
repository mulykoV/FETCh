using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly FETChDbContext _context;

        public AdminCategoriesController(FETChDbContext context)
        {
            _context = context;
        }

        // GET: /AdminCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.CourseCategories
                .Include(c => c.Courses) // щоб бачити кількість курсів у категорії
                .ToListAsync();
            return View(categories);
        }

        // GET: /AdminCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.CourseCategories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: /AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /AdminCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.CourseCategories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.CourseCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: /AdminCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseCategory category)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.CourseCategories.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.CourseCategories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: /AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.CourseCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            // Можна додати перевірку: якщо у категорії є курси, не дозволяти видаляти
            if (await _context.Courses.AnyAsync(c => c.CategoryId == id))
            {
                ModelState.AddModelError("", "Неможливо видалити категорію, яка містить курси.");
                return View(category);
            }

            _context.CourseCategories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
