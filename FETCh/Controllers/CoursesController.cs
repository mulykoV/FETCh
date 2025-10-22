using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FetchData.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IFETChRepository _repository;

        public CoursesController(IFETChRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<IActionResult> Index()
        {
            var courses = await _repository.ReadAll<Course>().ToListAsync();
            return View(courses);
        }

        // Форма створення курсу
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Додати курс
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
    }
}
