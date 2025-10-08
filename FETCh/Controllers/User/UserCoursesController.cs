using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly FETChDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCoursesController(FETChDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // -------------------- СПИСОК КУРСІВ --------------------
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Modules)
                .ToListAsync();
            return View(courses);
        }

        // -------------------- ДЕТАЛІ КУРСУ --------------------
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();
            return View(course);
        }

        // -------------------- ЗАПИС НА КУРС --------------------
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Enroll(int courseId)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null) return RedirectToAction("Login", "Account");

        //    var exists = await _context.UserCourses
        //        .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        //    if (exists != null)
        //    {
        //        TempData["Message"] = "Ви вже записані на цей курс!";
        //        return RedirectToAction("Details", new { id = courseId });
        //    }

        //    _context.UserCourses.Add(new UserCourse
        //    {
        //        UserId = userId,
        //        CourseId = courseId,
        //        EnrolledDate = DateTime.UtcNow,
        //        IsCompleted = false
        //    });

        //    await _context.SaveChangesAsync();
        //    TempData["Message"] = "Ви успішно записалися на курс!";
        //    return RedirectToAction("MyCourses");
        //}

        // -------------------- МОЇ КУРСИ --------------------
        public async Task<IActionResult> MyCourses()
        {
            var userId = _userManager.GetUserId(User);
            var enrollments = await _context.UserCourses
                .Include(uc => uc.Course)
                .ThenInclude(c => c.Modules)
                .Where(uc => uc.UserId == userId)
                .ToListAsync();

            return View(enrollments);
        }
    }
}
