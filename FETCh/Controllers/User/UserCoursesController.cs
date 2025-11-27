using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace FETCh.Controllers
{
    [Authorize]
    public class UserCoursesController : Controller
    {
        private readonly IFETChRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PAGE_SIZE = 1;
        public UserCoursesController(IFETChRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // -------------------- СПИСОК КУРСІВ --------------------
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _repository.GetAllCoursesAsync();
            var paged = courses
                .OrderBy(c => c.Id)
                .ToPagedList(page, PAGE_SIZE);

            return View(paged);
        }

        // -------------------- ДЕТАЛІ КУРСУ --------------------
        public async Task<IActionResult> Details(int id)
        {
            var course = await _repository.GetCourseDetailsAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // -------------------- ЗАПИС НА КУРС --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Login", "Account");

            var alreadyEnrolled = await _repository.IsUserEnrolledAsync(courseId, userId);
            if (alreadyEnrolled)
            {
                TempData["Message"] = "Ви вже записані на цей курс!";
                return RedirectToAction("Details", new { id = courseId });
            }

            await _repository.EnrollUserAsync(courseId, userId);
            TempData["Message"] = "Ви успішно записалися на курс!";
            return RedirectToAction("MyCourses");
        }

        // -------------------- МОЇ КУРСИ --------------------
        public async Task<IActionResult> MyCourses()
        {
            var userId = _userManager.GetUserId(User);
            var courses = await _repository.GetUserCoursesAsync(userId);
            return View(courses); 
        }
    }
}
