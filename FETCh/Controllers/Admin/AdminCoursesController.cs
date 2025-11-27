using FETCh.Authorization;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace FETCh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCoursesController : Controller
    {
        private readonly IFETChRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private const int PAGE_SIZE = 1;

        public AdminCoursesController(
            IFETChRepository repository,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // INDEX
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _repository.GetAllCoursesAsync();

            var pagedCourses = courses
                .OrderBy(c => c.Id)
                .ToPagedList(page, PAGE_SIZE);

            return View(pagedCourses);
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
                model.AuthorId = _userManager.GetUserId(User);

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

            // Ресурсна авторизація
            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            ViewBag.Categories = await _repository.GetAllCategoriesAsync();
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course updated)
        {
            if (id != updated.Id) return NotFound();

            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                updated.AuthorId = course.AuthorId;

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
            var authResult = await _authorizationService
                    .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

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
