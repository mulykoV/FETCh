using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminModulesController : Controller
    {
        private readonly IFETChRepository _repository;

        public AdminModulesController(IFETChRepository repository)
        {
            _repository = repository;
        }

        // -------------------- CREATE --------------------
        public IActionResult Create(int courseId)
        {
            var module = new Module { CourseId = courseId };
            return View(module);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Module module)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddModuleAsync(module);
                return RedirectToAction("Details", "AdminCourses", new { id = module.CourseId });
            }
            return View(module);
        }

        // -------------------- EDIT --------------------
        public async Task<IActionResult> Edit(int id)
        {
            var module = await _repository.GetModuleByIdAsync(id);
            if (module == null) return NotFound();
            return View(module);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Module module)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateModuleAsync(module);
                return RedirectToAction("Details", "AdminCourses", new { id = module.CourseId });
            }
            return View(module);
        }

        // -------------------- DELETE --------------------
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _repository.GetModuleByIdAsync(id);
            if (module == null) return NotFound();
            return View(module);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _repository.GetModuleByIdAsync(id);
            if (module == null) return NotFound();

            int courseId = module.CourseId;
            await _repository.DeleteModuleAsync(id);

            return RedirectToAction("Details", "AdminCourses", new { id = courseId });
        }

        // -------------------- DETAILS --------------------
        public async Task<IActionResult> Details(int id)
        {
            var module = await _repository.GetModuleByIdAsync(id);
            if (module == null) return NotFound();
            return View(module);
        }
    }
}
