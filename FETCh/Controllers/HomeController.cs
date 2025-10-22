using System.Diagnostics;
using FETCh.Configurations;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace FETCh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFETChRepository _repository;
        private readonly AppConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IFETChRepository repository, AppConfiguration config, IWebHostEnvironment env)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
            _env = env;
        }

        // -------------------- ГОЛОВНА СТОРІНКА --------------------
        public async Task<IActionResult> Index()
        {
            // Наприклад, показуємо список усіх курсів
            var courses = await _repository.GetAllCoursesAsync();
            return View(courses); // передаємо курси у View
        }

        // -------------------- ПРО ПОЛІТИКУ КОНФІДЕНЦІЙНОСТІ --------------------
        public IActionResult Privacy()
        {
            return View();
        }

        // -------------------- СТОРІНКА ПОМИЛКИ --------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }


        [HttpGet]
        public IActionResult Info()
        {
            return Json(new
            {
                Environment = _env.EnvironmentName,
                Application = _config.ApplicationName,
                Mode = _config.ProjectSettings.Mode,
                EnableNotifications = _config.ProjectSettings.EnableNotifications,
                ConnectionString = _config.ConnectionStrings.DefaultConnection
            });
        }

        [HttpPost]
        public IActionResult ToggleMode()
        {
            if (_config.ProjectSettings.Mode == "Development")
                _config.ProjectSettings.Mode = "Production";
            else
                _config.ProjectSettings.Mode = "Development";

            return Json(new { Mode = _config.ProjectSettings.Mode });
        }
    }
}
