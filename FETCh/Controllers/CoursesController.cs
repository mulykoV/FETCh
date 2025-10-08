using FetchData.Data.Repositories;
using FETChModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace FetchData.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Отримати список курсів
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            return View(courses);
        }

        // Додати курс
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Courses.AddAsync(course);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
    }
}
