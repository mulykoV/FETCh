using FETCh.Authorization;
using FETCh.Models.ViewModels;
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

        // AJAX: Перевірка унікальності назви курсу
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckTitle(string title)
        {
            // Правильний асинхронний виклик
            var allCourses = await _repository.GetAllCoursesAsync();

            // Перевірка в пам'яті (якщо репозиторій повертає List/IEnumerable)
            bool exists = allCourses.Any(c => c.Title.ToLower() == title.ToLower());

            if (exists)
            {
                return Json($"Назва '{title}' вже використовується.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckPriceLogic(decimal price, bool isFree)
        {
            // Ситуація 1: Курс позначено як безкоштовний, але вказана ціна
            if (isFree && price > 0)
            {
                return Json("Безкоштовний курс повинен мати ціну 0.00");
            }

            // Ситуація 2: Курс платний (галочка знята), але ціна 0
            if (!isFree && price <= 0)
            {
                return Json("Платний курс не може коштувати 0.00. Вкажіть ціну або позначте як 'Безкоштовний'.");
            }

            return Json(true);
        }
    }
}
