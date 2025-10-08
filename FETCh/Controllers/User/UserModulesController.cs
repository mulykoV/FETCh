using FETChModels.Models;
using FetchData.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserModulesController : Controller
    {
        private readonly FETChDbContext _context;

        public UserModulesController(FETChDbContext context)
        {
            _context = context;
        }

        // GET: /UserModules?courseId=5
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

        // GET: /UserModules/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var module = await _context.Modules
                .Include(m => m.Lectures)
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null) return NotFound();

            return View(module);
        }
    }
}
