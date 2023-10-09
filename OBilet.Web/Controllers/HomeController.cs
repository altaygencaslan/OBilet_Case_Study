using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using OBilet.Core.Business.Abstract;
using OBilet.Core.Cache.Abstract;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IOBiletManager oBiletManager, IHttpContextAccessor contextAccessor, IMemoryCacheManager memoryCacheManager)
            : base(contextAccessor, memoryCacheManager, oBiletManager)
        {
            _logger = logger;
            _oBiletManager = oBiletManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> IndexAsync(int? _originId, int? _destinationId, DateTime? _departureDate)
        {
            var busLocationsResponse = await GetBusLocationsTryGetCache();
            if (!busLocationsResponse.IsSuccess)
            {
                return View();
            }

            var model = GetDefaultsFromSessionOrParameters(_originId, _destinationId, _departureDate, busLocationsResponse.Data);
            model.OriginLocations = busLocationsResponse.Data
                                                        //.Where(w => w.CountryId == 8)
                                                        .Select(s => new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = s.LongName,
                                                            Selected = s.Id == model.OriginId
                                                        })
                                                        .ToArray();

            model.DestinationLocations = busLocationsResponse.Data
                                                             //.Where(w => w.CountryId == 8)
                                                             .Select(s => new SelectListItem
                                                             {
                                                                 Value = s.Id.ToString(),
                                                                 Text = s.LongName,
                                                                 Selected = s.Id == model.DestinationId
                                                             })
                                                             .ToArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(BusLocationSearchModel model)
        {
            if (ModelState.IsValid)
            {
                string searchModel = JsonSerializer.Serialize(model);
                _contextAccessor.HttpContext.Session.SetString("BusLocationSearchModel", searchModel);

                return RedirectToAction("JourneyIndex", new BusJourneysRequestScreenDto
                {
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId,
                    DepartureDate = model.DepartureDate,
                });
            }


            var busLocationsResponse = await GetBusLocationsTryGetCache();
            model.OriginLocations = busLocationsResponse.Data
                                                        .Select(s => new SelectListItem
                                                        {
                                                            Value = s.Id.ToString(),
                                                            Text = s.LongName,
                                                            Selected = s.Id == model.OriginId
                                                        })
                                                        .ToArray();

            model.DestinationLocations = busLocationsResponse.Data
                                                             .Select(s => new SelectListItem
                                                             {
                                                                 Value = s.Id.ToString(),
                                                                 Text = s.LongName,
                                                                 Selected = s.Id == model.DestinationId
                                                             })
                                                             .ToArray();

            return View(model);
        }

        public async Task<IActionResult> JourneyIndexAsync(BusJourneysRequestScreenDto model)
        {
            throw new Exception("test");

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

            var busLocationsResponse = await GetBusLocationsTryGetCache();

            BusJourneysResponseScreenDto response = new BusJourneysResponseScreenDto();
            response.Data = journeysResponse.Data.OrderBy(o => o.Rank).ToArray();
            response.OriginId = model.OriginId;
            response.OriginName = busLocationsResponse.Data?.FirstOrDefault(f => f.Id == model.OriginId)?.Name ?? "OriginName";
            response.DestinationId = model.DestinationId;
            response.DestinationName = busLocationsResponse.Data?.FirstOrDefault(f => f.Id == model.DestinationId)?.Name ?? "DestinationName";
            response.DepartureDate = DateTime.Parse(model.DepartureDate);

            return View(response);
        }
    }
}