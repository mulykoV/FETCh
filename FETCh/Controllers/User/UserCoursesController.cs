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
        private const int PAGE_SIZE = 5;
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
        // 1. ОНОВЛЕНИЙ МЕТОД DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var course = await _repository.GetCourseDetailsAsync(id);
            if (course == null) return NotFound();

            var userId = _userManager.GetUserId(User);

            // Логіка перевірки статусу
            bool isEnrolled = false;
            bool isCompleted = false;

            if (userId != null)
            {
                // Отримуємо курсі користувача, щоб перевірити статус
                // (Припускаємо, що у репозиторії є метод отримання курсів юзера)
                var userCourses = await _repository.GetUserCoursesAsync(userId);

                // Шукаємо цей конкретний курс у списку користувача
                var currentEnrollment = userCourses.FirstOrDefault(uc => uc.CourseId == id); //

                if (currentEnrollment != null)
                {
                    isEnrolled = true;
                    isCompleted = currentEnrollment.IsCompleted; //
                }
            }

            // Передаємо дані у View через ViewBag
            ViewBag.IsEnrolled = isEnrolled;
            ViewBag.IsCompleted = isCompleted;

            return View(course);
        }

        // 2. НОВИЙ МЕТОД ДЛЯ ЗАВЕРШЕННЯ КУРСУ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteCourse(int courseId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Login", "Account");

            // Викликаємо метод репозиторію (його треба додати, див. Крок 2)
            await _repository.MarkCourseAsCompletedAsync(userId, courseId);

            TempData["Message"] = "Вітаємо! Місію успішно завершено!";

            // Повертаємо на сторінку "Мої курси", щоб побачити зелений статус
            return RedirectToAction("MyCourses");
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
