using FETCh.Authorization;
using FETCh.Models.ViewModels;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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

        // ЗМІНЕНО: Тепер локалізатор прив'язаний до цього контролера
        private readonly IStringLocalizer<AdminCoursesController> _localizer;

        private const int PAGE_SIZE = 5;

        public AdminCoursesController(
            IFETChRepository repository,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService,
            // ЗМІНЕНО: Інжектуємо правильний локалізатор
            IStringLocalizer<AdminCoursesController> localizer)
        {
            _repository = repository;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _localizer = localizer;
        }

        // INDEX
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _repository.GetAllCoursesAsync();
            var pagedCourses = courses.OrderBy(c => c.Id).ToPagedList(page, PAGE_SIZE);
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

        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CourseViewModel
            {
                Categories = await _repository.GetAllCategoriesAsync()
            };
            return View(vm);
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Title = vm.Title,
                    Subtitle = vm.Subtitle,
                    Description = vm.Description,
                    Language = vm.Language,
                    IsFree = vm.IsFree,
                    Price = vm.Price,
                    PriceCurrency = vm.PriceCurrency,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    ImageUrl = vm.ImageUrl,
                    BannerImageUrl = vm.BannerImageUrl,
                    DurationHours = vm.DurationHours,
                    ContactEmail = vm.ContactEmail,
                    CategoryId = vm.CategoryId,
                    AuthorId = _userManager.GetUserId(User)
                };

                await _repository.AddCourseAsync(course);

                // ЗМІНЕНО: Використовуємо _localizer замість _sharedLocalizer
                TempData["Message"] = _localizer["CourseCreatedSuccess"].Value;
                return RedirectToAction(nameof(Index));
            }

            vm.Categories = await _repository.GetAllCategoriesAsync();
            return View(vm);
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded) return Forbid();

            var vm = new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                Description = course.Description,
                Language = course.Language,
                IsFree = course.IsFree,
                Price = course.Price,
                PriceCurrency = course.PriceCurrency,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                ImageUrl = course.ImageUrl,
                BannerImageUrl = course.BannerImageUrl,
                DurationHours = course.DurationHours,
                ContactEmail = course.ContactEmail,
                ConfirmContactEmail = course.ContactEmail,
                CategoryId = course.CategoryId,
                Categories = await _repository.GetAllCategoriesAsync()
            };

            return View(vm);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded) return Forbid();

            if (ModelState.IsValid)
            {
                course.Title = vm.Title;
                course.Subtitle = vm.Subtitle;
                course.Description = vm.Description;
                course.Language = vm.Language;
                course.IsFree = vm.IsFree;
                course.Price = vm.Price;
                course.PriceCurrency = vm.PriceCurrency;
                course.StartDate = vm.StartDate;
                course.EndDate = vm.EndDate;
                course.ImageUrl = vm.ImageUrl;
                course.BannerImageUrl = vm.BannerImageUrl;
                course.DurationHours = vm.DurationHours;
                course.ContactEmail = vm.ContactEmail;
                course.CategoryId = vm.CategoryId;

                await _repository.UpdateCourseAsync(course);

                // ЗМІНЕНО: Використовуємо _localizer замість _sharedLocalizer
                TempData["Message"] = _localizer["CourseUpdatedSuccess"].Value;
                return RedirectToAction(nameof(Index));
            }

            vm.Categories = await _repository.GetAllCategoriesAsync();
            return View(vm);
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var authResult = await _authorizationService
                .AuthorizeAsync(User, course, new IsCourseAuthorRequirement());

            if (!authResult.Succeeded) return Forbid();

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

            if (!authResult.Succeeded) return Forbid();

            await _repository.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ENROLL
        public async Task<IActionResult> EnrollUser(int courseId, string userId)
        {
            if (await _repository.IsUserEnrolledAsync(courseId, userId))
            {
                // ЗМІНЕНО: _localizer
                TempData["Message"] = _localizer["AlreadyEnrolled"].Value;
            }
            else
            {
                await _repository.EnrollUserAsync(courseId, userId);
                // ЗМІНЕНО: _localizer
                TempData["Message"] = _localizer["UserEnrolled"].Value;
            }

            return RedirectToAction("Details", new { id = courseId });
        }

        // AJAX VALIDATION
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckTitle(string title)
        {
            var exists = (await _repository.GetAllCoursesAsync())
                .Any(c => c.Title.ToLower() == title.ToLower());

            if (exists)
                // ЗМІНЕНО: _localizer
                return Json(_localizer["TitleTaken", title].Value);

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckPriceLogic(decimal price, bool isFree)
        {
            if (isFree && price > 0)
                return Json(_localizer["FreeCoursePriceError"].Value);

            if (!isFree && price <= 0)
                return Json(_localizer["PaidCoursePriceError"].Value);

            return Json(true);
        }
    }
}