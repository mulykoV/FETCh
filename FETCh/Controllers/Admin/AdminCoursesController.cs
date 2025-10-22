using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCoursesController : Controller
    {
        private readonly IFETChRepository _repository;

        public AdminCoursesController(IFETChRepository repository)
        {
            _repository = repository;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var courses = await _repository.GetAllCoursesAsync();
            return View(courses);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            ViewBag.Users = await _repository.GetAllUsersAsync();
            return View(course);
        }

        // CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _repository.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course model)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddCourseAsync(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _repository.GetAllCategoriesAsync();
            return View(model);
        }

        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            ViewBag.Categories = await _repository.GetAllCategoriesAsync();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course updated)
        {
            if (id != updated.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateCourseAsync(updated);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _repository.GetAllCategoriesAsync();
            return View(updated);
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ENROLL
        public async Task<IActionResult> EnrollUser(int courseId, string userId)
        {
            if (await _repository.IsUserEnrolledAsync(courseId, userId))
            {
                TempData["Message"] = "Користувач уже записаний на цей курс!";
            }
            else
            {
                await _repository.EnrollUserAsync(courseId, userId);
                TempData["Message"] = "Користувач успішно записаний на курс!";
            }

            return RedirectToAction("Details", new { id = courseId });
        }
    }
}
