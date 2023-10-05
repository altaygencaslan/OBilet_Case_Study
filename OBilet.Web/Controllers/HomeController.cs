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

        public async Task<IActionResult> IndexAsync()
        {
            var browserInfo = HttpContext.Request.Headers.UserAgent.FirstOrDefault();

            var session = await _oBiletService.GetSessionAsync(new Integration.Services.Model.Request.GetSessionRequest
            {
                Connection = new Integration.Services.Model.Request.Connection
                {
                    IpAddress = "165.114.41.21", //HttpContext.Connection.LocalIpAddress.ToString(),
                    Port = "5117", //HttpContext.Connection.LocalPort.ToString(),
                },
                Browser = new Integration.Services.Model.Request.Browser
                {
                    Name = "Chrome", //browserInfo,
                    Version = "47.0.0.12", //browserInfo.Substring(browserInfo.LastIndexOf("/") + 1)
                }
            });

            return View();
        }

        public IActionResult JourneyIndex()
        {
            return View();
        }

    }
}