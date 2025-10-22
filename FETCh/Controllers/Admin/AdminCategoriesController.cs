using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly IFETChRepository _repository;

        public AdminCategoriesController(IFETChRepository repository)
        {
            _repository = repository;
        }

        // GET: /AdminCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: /AdminCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
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
                await _repository.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
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
                await _repository.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: /AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: /AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            if (await _repository.CategoryHasCoursesAsync(id))
            {
                ModelState.AddModelError("", "Неможливо видалити категорію, яка містить курси.");
                return View(category);
            }

            await _repository.DeleteCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
