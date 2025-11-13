using FETCh.Authorization;
using FETCh.Configurations;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FETCh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFETChRepository _repository;
        private readonly AppConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, 
                                IFETChRepository repository, 
                                AppConfiguration config, 
                                IWebHostEnvironment env,
                                UserManager<ApplicationUser> userManager,
                                IAuthorizationService authorizationService)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
            _env = env;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // -------------------- √ŒÀŒ¬Õ¿ —“Œ–≤Õ ¿ --------------------
        public async Task<IActionResult> Index()
        {
            // Õ‡ÔËÍÎ‡‰, ÔÓÍ‡ÁÛ∫ÏÓ ÒÔËÒÓÍ ÛÒ≥ı ÍÛÒ≥‚
            var courses = await _repository.GetAllCoursesAsync();
            return View(courses); // ÔÂÂ‰‡∫ÏÓ ÍÛÒË Û View
        }

        // -------------------- œ–Œ œŒÀ≤“» ”  ŒÕ‘≤ƒ≈Õ÷≤…ÕŒ—“≤ --------------------
        public IActionResult Privacy()
        {
            return View();
        }

        // -------------------- —“Œ–≤Õ ¿ œŒÃ»À » --------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }


        // -------------------- —“Œ–≤Õ ¿ œ–≈Ã≤”Ã --------------------
        [Authorize(Policy = "VerifiedClientOnly")]
        [HttpGet]
        public async Task<IActionResult> Premium()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, null, "PremiumOnly");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return View(); // œÓÍ‡ÁÛ∫ÏÓ ÒÚÓ≥ÌÍÛ Premium
        }

        // -------------------- —“Œ–≤Õ ¿ ‘Œ–”Ã --------------------
        [Authorize(Policy = "VerifiedClientOnly")]
        [HttpGet]
        public async Task<IActionResult> Forum()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, null, "ForumAccess");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return View();
        }

        //[Authorize]
        [Authorize(Policy = "VerifiedClientOnly")]
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
