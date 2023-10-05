using Mapster;
using Microsoft.Extensions.Logging;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.GetBusJourneys;
using OBilet.Core.DTO.GetBusLocations;
using OBilet.Core.DTO.GetSession;
using OBilet.Integration.Services.Abstract;
using OBilet.Integration.Services.Model.Base;
using OBilet.Integration.Services.Model.Request;
using OBilet.Integration.Services.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.Business.Concrete
{
    public class OBiletManager : IOBiletManager
    {
        private readonly IOBiletService _oBiletService;
        private readonly ILogger<OBiletManager> _logger;

        public OBiletManager(IOBiletService oBiletService, ILogger<OBiletManager> logger)
        {
            _oBiletService = oBiletService;
            _logger = logger;
        }

        public async Task<GeneralResponse<BusLocationsResponseDto>> GetBusLocationsAsync(BusLocationRequestDto model)
        {
            GetBusLocationsRequest request = model.Adapt<GetBusLocationsRequest>();
            var buslocations = await _oBiletService.GetBusLocationsAsync(request);
            if (!buslocations.IsSuccess)
            {
                return new GeneralResponse<BusLocationsResponseDto>();
            }

            return buslocations.Data.Adapt<GeneralResponse<BusLocationsResponseDto>>();
        }

        public async Task<GeneralResponse<BusJourneysResponseDto>> GetJourneysAsync(BusJourneysRequestDto model)
        {
            GetBusJourneysRequest request = model.Adapt<GetBusJourneysRequest>();
            var journeys = await _oBiletService.GetJourneysAsync(request);
            if (!journeys.IsSuccess)
            {
                return new GeneralResponse<BusJourneysResponseDto>();
            }

            BusJourneysResponseDto response = model.Adapt<BusJourneysResponseDto>();
            response.Data = journeys.Data.Adapt<BusJourneysData[]>();

            return new GeneralResponse<BusJourneysResponseDto>(response);
        }

        public async Task<GeneralResponse<SessionResponseDto>> GetSessionAsync(SessionRequestDto model)
        {
            GetSessionRequest request = model.Adapt<GetSessionRequest>();
            var session = await _oBiletService.GetSessionAsync(request);
            if (!session.IsSuccess)
            {
                return new GeneralResponse<SessionResponseDto>();
            }

            SessionResponseDto response = session.Data.Adapt<SessionResponseDto>();
            return new GeneralResponse<SessionResponseDto>(response);
        }
    }
}
