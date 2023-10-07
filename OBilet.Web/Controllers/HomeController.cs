using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.Base;
using OBilet.Core.DTO.GetBusJourneys;
using OBilet.Core.DTO.GetBusLocations;
using OBilet.Core.DTO.GetSession;
using OBilet.Integration.Services.Abstract;
using OBilet.Integration.Services.Model.Base;
using OBilet.Integration.Services.Model.Request;
using OBilet.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace OBilet.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOBiletManager _oBiletManager;

        public HomeController(ILogger<HomeController> logger, IOBiletManager oBiletManager)
        {
            _logger = logger;
            _oBiletManager = oBiletManager;
        }

        public async Task<IActionResult> IndexAsync(int? _originId, int? _destinationId, DateTime? _departureDate)
        {
            var busLocationsResponse = await _oBiletManager.GetBusLocationsAsync(new GeneralRequestDto<BusLocationRequestDto>
            {
                SessionRequest = GetSessionDefault(),
                RequestItem =
                new BusLocationRequestDto
                {
                    Date = DateTime.Now,
                }
            });

            if (!busLocationsResponse.IsSuccess)
            {
                return View();
            }

            int originId = _originId.HasValue ? _originId.Value : busLocationsResponse.Data.FirstOrDefault(f => f.Rank == 1).Id;
            int destinationId = _destinationId.HasValue ? _destinationId.Value : busLocationsResponse.Data.FirstOrDefault(f => f.Rank == 3).Id;
            string departureDate = _departureDate.HasValue ? _departureDate.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");

            var model = new BusLocationSearchModel
            {
                DepartureDate = departureDate,
                OriginId = originId,
                DestionationId = destinationId,
                OriginLocations = busLocationsResponse.Data.Where(w => w.CountryId == 8).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.LongName,
                    Selected = s.Id == originId
                }).ToArray(),
                DestinationLocations = busLocationsResponse.Data.Where(w => w.CountryId == 8).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.LongName,
                    Selected = s.Id == destinationId
                }).ToArray(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(BusLocationSearchModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("JourneyIndex", new BusJourneysRequestScreenDto
                {
                    OriginId = model.OriginId,
                    DestinationId = model.DestionationId,
                    DepartureDate = model.DepartureDate,
                });
            }

            return View(model);
        }

        public async Task<IActionResult> JourneyIndexAsync(BusJourneysRequestScreenDto model)
        {
            var journeysResponse = await _oBiletManager.GetJourneysAsync(new GeneralRequestDto<BusJourneysRequestDto>
            {
                SessionRequest = GetSessionDefault(),
                RequestItem = new BusJourneysRequestDto
                {
                    DepartureDate = DateTime.Parse(model.DepartureDate),
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId,
                }
            });

            if (!journeysResponse.IsSuccess)
            {
                return View();
            }

            BusJourneysResponseScreenDto response = new BusJourneysResponseScreenDto();
            response.Data = journeysResponse.Data.OrderBy(o => o.Rank).ToArray();
            response.OriginId = model.OriginId;
            response.DestinationId = model.DestinationId;
            response.OriginName = "OriginName";
            response.DestinationName = "DestinationName";
            response.DepartureDate = DateTime.Parse(model.DepartureDate);

            return View(response);
        }

    }
}