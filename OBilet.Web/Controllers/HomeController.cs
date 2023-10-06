using Microsoft.AspNetCore.Mvc;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.GetSession;
using OBilet.Integration.Services.Abstract;
using OBilet.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace OBilet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOBiletManager _oBiletManager;

        public HomeController(ILogger<HomeController> logger, IOBiletManager oBiletManager)
        {
            _logger = logger;
            _oBiletManager = oBiletManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            await _oBiletManager.GetBusLocationsAsync(new Core.DTO.Base.GeneralRequestDto<Core.DTO.GetBusLocations.BusLocationRequestDto>
            {
                SessionRequest = new SessionRequestDto
                {
                    Connection = new ConnectionDto
                    {
                        IpAddress = "165.114.41.21",
                        Port = "5117",
                    },
                    Browser = new BrowserDto
                    {
                        Name = "Chrome",
                        Version = "47.0.0.12",
                    },
                },

                RequestItem =
                new Core.DTO.GetBusLocations.BusLocationRequestDto
                {
                    Date = DateTime.Now,
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