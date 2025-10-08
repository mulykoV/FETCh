using FetchData.Data;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FETCh.Controllers.User
{
    [Authorize(Roles = "User")]
    public class UserLecturesController : Controller
    {
        private readonly FETChDbContext _context;

        public UserLecturesController(FETChDbContext context)
        {
            _context = context;
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
    }
}
