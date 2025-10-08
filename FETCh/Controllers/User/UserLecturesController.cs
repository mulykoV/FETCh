using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserLecturesController : Controller
    {
        private readonly FETChDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserLecturesController(FETChDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Перегляд лекції
        public async Task<IActionResult> Details(int id)
        {
            var lecture = await _context.Lectures
                .Include(l => l.Module)
                    .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == id);

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

            var progress = await _context.UserLectureProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LectureId == lectureId);

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
                _context.UserLectureProgresses.Add(progress);
            }
            else
            {
                progress.Watched = true;
                progress.WatchedDate = DateTime.UtcNow;
                progress.ProgressPercentage = 100;
                _context.UserLectureProgresses.Update(progress);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "✅ Лекцію позначено як переглянуту!";
            return RedirectToAction("Details", new { id = lectureId });
        }

    }
}
