using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IOBiletManager oBiletManager, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _oBiletManager = oBiletManager;
            _contextAccessor = contextAccessor;
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

            var model = GetFromCacheOrParam(_originId, _destinationId, _departureDate, busLocationsResponse.Data);
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

        private BusLocationSearchModel GetFromCacheOrParam(int? _originId, int? _destinationId, DateTime? _departureDate, BusLocationsResponseDto[] _busLocations)
        {
            BusLocationSearchModel searchModel = new BusLocationSearchModel();

            if (_destinationId.HasValue && _originId.HasValue && _departureDate.HasValue)
            {
                searchModel.OriginId = _destinationId.Value;
                searchModel.DestinationId = _originId.Value;
                searchModel.DepartureDate = _departureDate.Value.ToString("dd.MM.yyyy");
            }
            else
            {
                var searchModelJSON = _contextAccessor.HttpContext.Session.GetString("BusLocationSearchModel");
                if (!string.IsNullOrEmpty(searchModelJSON))
                {
                    searchModel = JsonSerializer.Deserialize<BusLocationSearchModel>(searchModelJSON);
                }
                else
                {
                    searchModel.OriginId = _busLocations.FirstOrDefault(f => f.Rank == 1)?.Id ?? 0;
                    searchModel.DestinationId = _busLocations.FirstOrDefault(f => f.Rank == 3)?.Id ?? 0;
                    searchModel.DepartureDate = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy");

                }
            }

            return searchModel;
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