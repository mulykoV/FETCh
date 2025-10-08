using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLecturesController : Controller
    {
        private readonly FETChDbContext _context;

        public AdminLecturesController(FETChDbContext context)
        {
            _context = context;
        }

        // -------------------- СТВОРЕННЯ ЛЕКЦІЇ --------------------
        public IActionResult Create(int moduleId)
        {
            var lecture = new Lecture { ModuleId = moduleId };
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _context.Lectures.Add(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "AdminModules", new { id = lecture.ModuleId });
            }
            return View(lecture);
        }

        // -------------------- РЕДАГУВАННЯ ЛЕКЦІЇ --------------------
        public async Task<IActionResult> Edit(int id)
        {
            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture == null) return NotFound();
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _context.Lectures.Update(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "AdminModules", new { id = lecture.ModuleId });
            }
            return View(lecture);
        }

        // -------------------- ВИДАЛЕННЯ ЛЕКЦІЇ --------------------
        public async Task<IActionResult> Delete(int id)
        {
            var lecture = await _context.Lectures
                .Include(l => l.Module)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (lecture == null) return NotFound();
            return View(lecture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture == null) return NotFound();

            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "AdminModules", new { id = lecture.ModuleId });
        }

        // -------------------- ДЕТАЛІ ЛЕКЦІЇ --------------------
        public async Task<IActionResult> Details(int id)
        {
            var lecture = await _context.Lectures
                .Include(l => l.Module)
                .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lecture == null) return NotFound();
            return View(lecture);
        }
    }
}
