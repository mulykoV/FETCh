using FETCh.Configurations;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization; 
using FETCh.Authorization;
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
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, 
                                IFETChRepository repository, 
                                AppConfiguration config, 
                                IWebHostEnvironment env,
                                UserManager<ApplicationUser> userManager,
                                IAuthorizationService authorizationService,  
                                IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
            _env = env;
            _localizer = localizer;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // -------------------- ÃÎËÎÂÍÀ ÑÒÎÐ²ÍÊÀ --------------------
        public async Task<IActionResult> Index()
        {
            
            // Îòðèìóºìî ðÿäîê ç ðåñóðñó çà êëþ÷åì
            ViewData["Message"] = _localizer["WelcomeMessage"];
            ViewData["subtext1"] = _localizer["subtext1"];
            // Íàïðèêëàä, ïîêàçóºìî ñïèñîê óñ³õ êóðñ³â
            var courses = await _repository.GetAllCoursesAsync();
            return View(courses); // ïåðåäàºìî êóðñè ó View
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            // Âñòàíîâëþºìî êóêè
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        // -------------------- ÏÐÎ ÏÎË²ÒÈÊÓ ÊÎÍÔ²ÄÅÍÖ²ÉÍÎÑÒ² --------------------
        public IActionResult Privacy()
        {
            return View();
        }

        // -------------------- ÑÒÎÐ²ÍÊÀ ÏÎÌÈËÊÈ --------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }


        // -------------------- ÑÒÎÐ²ÍÊÀ ÏÐÅÌ²ÓÌ --------------------
        [Authorize(Policy = "VerifiedClientOnly")]
        [HttpGet]
        public async Task<IActionResult> Premium()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, null, "PremiumOnly");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            return View(); // Ïîêàçóºìî ñòîð³íêó Premium
        }

        // -------------------- ÑÒÎÐ²ÍÊÀ ÔÎÐÓÌ --------------------
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
