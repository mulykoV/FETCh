using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserModulesController : Controller
    {
        private readonly FETChDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserModulesController(FETChDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var modules = await _context.Modules
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Lectures)
                .ToListAsync();

            ViewBag.CourseId = courseId;
            ViewBag.CourseTitle = (await _context.Courses.FindAsync(courseId))?.Title;
            return View(modules);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);

            var module = await _context.Modules
                .Include(m => m.Lectures)
                    .ThenInclude(l => l.UserLectureProgresses)
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null) return NotFound();


            ViewBag.UserId = userId;

            return View(module);
        }
    }
}
