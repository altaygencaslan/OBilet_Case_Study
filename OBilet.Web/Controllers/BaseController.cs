using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OBilet.Core.Business.Abstract;
using OBilet.Core.Business.Concrete;
using OBilet.Core.Cache.Abstract;
using OBilet.Core.DTO.Base;
using OBilet.Core.DTO.GetBusLocations;
using OBilet.Core.DTO.GetSession;
using OBilet.Integration.Services.Model.Base;
using OBilet.Web.Models;
using System;
using System.Text.Json;
using UAParser;

namespace OBilet.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMemoryCacheManager _memoryCacheManager;
        private readonly IOBiletManager _oBiletManager;

        public BaseController(IHttpContextAccessor contextAccessor, IMemoryCacheManager memoryCacheManager, IOBiletManager oBiletManager)
        {
            _contextAccessor = contextAccessor;
            _memoryCacheManager = memoryCacheManager;
            _oBiletManager = oBiletManager;
        }

        public SessionRequestDto GetSessionDefault()
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var port = Request.HttpContext.Connection.RemotePort;

            var userAgent = HttpContext.Request.Headers.UserAgent;
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);

            return new SessionRequestDto
            {
                Connection = new ConnectionDto
                {
                    IpAddress = ipAddress.ToString(),
                    Port = port.ToString(),
                },
                Browser = new BrowserDto
                {
                    Name = c.UA.Family,
                    Version = $"{c.UA.Major}.{c.UA.Minor}",
                },
            };
        }

        public BusLocationSearchModel GetDefaultsFromSessionOrParameters(int? _originId, int? _destinationId, DateTime? _departureDate, BusLocationsResponseDto[] _busLocations)
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

        public async Task<GeneralResponse<BusLocationsResponseDto[]>> GetBusLocationsTryGetCache(bool fromCache = true)
        {
            GeneralResponse<BusLocationsResponseDto[]>? busLocationsResponse = null;
            if (fromCache)
            {
                _memoryCacheManager.TryGetValue("BusLocationsResponseDto", out busLocationsResponse);
                if (busLocationsResponse != null)
                {
                    return busLocationsResponse;
                }
            }

            busLocationsResponse = await _oBiletManager.GetBusLocationsAsync(new GeneralRequestDto<BusLocationRequestDto>
            {
                SessionRequest = GetSessionDefault(),
                RequestItem =
                new BusLocationRequestDto
                {
                    Date = DateTime.Now,
                }
            });

            _memoryCacheManager.SetWithExpire("BusLocationsResponseDto", busLocationsResponse);

            return busLocationsResponse;
        }
    }
}
