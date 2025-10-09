using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLecturesController : Controller
    {
        private readonly IFETChRepository _repository;

        public AdminLecturesController(IFETChRepository repository)
        {
            _repository = repository;
        }

        // -------------------- CREATE --------------------
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
                await _repository.AddLectureAsync(lecture);
                return RedirectToAction("Details", "AdminModules", new { id = lecture.ModuleId });
            }
            return View(lecture);
        }

        // -------------------- EDIT --------------------
        public async Task<IActionResult> Edit(int id)
        {
            var lecture = await _repository.GetLectureByIdAsync(id);
            if (lecture == null) return NotFound();
            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateLectureAsync(lecture);
                return RedirectToAction("Details", "AdminModules", new { id = lecture.ModuleId });
            }
            return View(lecture);
        }

        // -------------------- DELETE --------------------
        public async Task<IActionResult> Delete(int id)
        {
            var lecture = await _repository.GetLectureByIdAsync(id);
            if (lecture == null) return NotFound();
            return View(lecture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = await _repository.GetLectureByIdAsync(id);
            if (lecture == null) return NotFound();

            int moduleId = lecture.ModuleId;
            await _repository.DeleteLectureAsync(id);
            return RedirectToAction("Details", "AdminModules", new { id = moduleId });
        }

        // -------------------- DETAILS --------------------
        public async Task<IActionResult> Details(int id)
        {
            var lecture = await _repository.GetLectureByIdAsync(id);
            if (lecture == null) return NotFound();
            return View(lecture);
        }
    }
}
