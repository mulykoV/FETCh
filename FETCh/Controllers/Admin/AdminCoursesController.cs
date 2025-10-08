using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminCoursesController : Controller
    {
        private readonly FETChDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminCoursesController(FETChDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // -------------------- INDEX --------------------
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .ToListAsync();
            return View(courses);
        }

        // -------------------- DETAILS --------------------
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);


            if (course == null) return NotFound();

            ViewBag.Users = await _userManager.Users.ToListAsync();
            return View(course);
        }

        // -------------------- CREATE --------------------
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.CourseCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course model)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.Categories = _context.CourseCategories.ToList();
            return View(model);
        }

        // -------------------- EDIT --------------------
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            ViewBag.Categories = _context.CourseCategories.ToList();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course updated)
        {
            if (id != updated.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updated);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.CourseCategories.ToList();

            return View(updated);
        }

        // -------------------- DELETE --------------------
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // -------------------- ЗАПИС НА КУРС --------------------

        public async Task<IActionResult> EnrollUser(int courseId, string userId)
        {
            // перевірка, чи користувач уже записаний
            var exists = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.CourseId == courseId && uc.UserId == userId);

            if (exists != null)
            {
                TempData["Message"] = "Користувач уже записаний на цей курс!";
                return RedirectToAction("Details", new { id = courseId });
            }

            _context.UserCourses.Add(new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
                EnrolledDate = DateTime.UtcNow,
                IsCompleted = false
            });

            await _context.SaveChangesAsync();
            TempData["Message"] = "Користувач успішно записаний на курс!";
            return RedirectToAction("Details", new { id = courseId });
        }

    }
}
