using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserModulesController : Controller
    {
        private readonly IFETChRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserModulesController(IFETChRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // -------------------- СПИСОК МОДУЛІВ КУРСУ --------------------
        public async Task<IActionResult> Index(int courseId)
        {
            var modules = await _repository.GetModulesByCourseIdAsync(courseId);
            var course = await _repository.GetCourseByIdAsync(courseId);

            if (modules == null || course == null)
                return NotFound();

            ViewBag.CourseId = courseId;
            ViewBag.CourseTitle = course.Title;
            return View(modules);
        }

        // -------------------- ДЕТАЛІ МОДУЛЮ --------------------
        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var module = await _repository.GetModuleByIdAsync(id);

            if (module == null)
                return NotFound();

            // Підвантажуємо прогрес користувача для кожної лекції
            foreach (var lecture in module.Lectures)
            {
                lecture.UserLectureProgresses = (await _repository
                    .GetLectureProgressForUserAsync(lecture.Id, userId)).ToList();
            }

            ViewBag.UserId = userId;
            return View(module);
        }
    }
}
