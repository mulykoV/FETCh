using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminModulesController : Controller
    {
        private readonly FETChDbContext _context;

        public AdminModulesController(FETChDbContext context)
        {
            _context = context;
        }

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
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "AdminCourses", new { id = module.CourseId });
            }
            return View(module);
        }

        // GET: /AdminModules/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module == null) return NotFound();
            return View(module);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Module module)
        {
            if (ModelState.IsValid)
            {
                _context.Modules.Update(module);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "AdminCourses", new { id = module.CourseId });
            }
            return View(module);
        }

        // GET: /AdminModules/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (module == null) return NotFound();

            return View(module);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module == null) return NotFound();

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "AdminCourses", new { id = module.CourseId });
        }

        // GET: /AdminModules/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var module = await _context.Modules
                .Include(m => m.Lectures)
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null) return NotFound();
            return View(module);
        }
    }
}
