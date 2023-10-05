using Microsoft.AspNetCore.Mvc;
using OBilet.Integration.Services.Abstract;
using OBilet.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace OBilet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOBiletService _oBiletService;

        public HomeController(ILogger<HomeController> logger, IOBiletService oBiletService)
        {
            _logger = logger;
            _oBiletService = oBiletService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JourneyIndex()
        {
            return View();
        }

    }
}