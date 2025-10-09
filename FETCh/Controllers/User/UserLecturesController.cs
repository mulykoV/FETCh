using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserLecturesController : Controller
    {
        private readonly IFETChRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserLecturesController(IFETChRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // Перегляд лекції
        public async Task<IActionResult> Details(int id)
        {
            var lecture = await _repository.GetLectureByIdAsync(id);
            if (lecture == null) return NotFound();

            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsWatched(int lectureId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Отримуємо лекцію (перевірка існування)
            var lecture = await _repository.GetLectureByIdAsync(lectureId);
            if (lecture == null)
                return NotFound();

            // Перевіряємо, чи є запис прогресу
            var progress = await _repository.GetUserLectureProgressAsync(userId, lectureId);
            if (progress == null)
            {
                progress = new UserLectureProgress
                {
                    UserId = userId,
                    LectureId = lectureId,
                    Watched = true,
                    WatchedDate = DateTime.UtcNow,
                    ProgressPercentage = 100
                };
                await _repository.AddUserLectureProgressAsync(progress);
            }
            else
            {
                progress.Watched = true;
                progress.WatchedDate = DateTime.UtcNow;
                progress.ProgressPercentage = 100;
                await _repository.UpdateUserLectureProgressAsync(progress);
            }

            TempData["Message"] = "✅ Лекцію позначено як переглянуту!";
            return RedirectToAction("Details", new { id = lectureId });
        }
    }
}
