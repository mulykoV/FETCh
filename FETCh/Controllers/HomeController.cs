using System.Diagnostics;
using FetchData.Interfaces;
using FETChModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace FETCh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFETChRepository _repository;

        public HomeController(ILogger<HomeController> logger, IFETChRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // -------------------- ������� ���в��� --------------------
        public async Task<IActionResult> Index()
        {
            // ���������, �������� ������ ��� �����
            var courses = await _repository.GetAllCoursesAsync();
            return View(courses); // �������� ����� � View
        }

        // -------------------- ��� ��˲���� ���Բ���ֲ����Ҳ --------------------
        public IActionResult Privacy()
        {
            return View();
        }

        // -------------------- ���в��� ������� --------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
