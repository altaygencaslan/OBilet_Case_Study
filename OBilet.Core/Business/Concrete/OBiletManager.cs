using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.Base;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace OBilet.Core.Business.Concrete
{
    public class OBiletManager : IOBiletManager
    {
        private readonly ILogger<OBiletManager> _logger;
        private readonly IOBiletService _oBiletService;
        private readonly IHttpContextAccessor _contextAccessor;

        public OBiletManager(IOBiletService oBiletService, ILogger<OBiletManager> logger, IHttpContextAccessor contextAccessor)
        {
            _oBiletService = oBiletService;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<SessionResponseDto> GetTokenFromSession(SessionRequestDto model)
        {
            string tokenJSON = string.Empty;
            _contextAccessor.HttpContext.Session.TryGetValue("token", out byte[] tokenByte);
            if (tokenByte == null)
            {
                var obiletSession = await GetSessionAsync(model);
                tokenJSON = JsonSerializer.Serialize(obiletSession.Data);
                tokenByte = Encoding.UTF8.GetBytes(tokenJSON);

                _contextAccessor.HttpContext.Session.Set("token", tokenByte);
            }

            tokenJSON = Encoding.UTF8.GetString(tokenByte);
            SessionResponseDto session = JsonSerializer.Deserialize<SessionResponseDto>(tokenJSON);
            return session;
        }

        public async Task<GeneralResponse<SessionResponseDto>> GetSessionAsync(SessionRequestDto model)
        {
            GetSessionRequest request = model.Adapt<GetSessionRequest>();
            var sessionResponse = await _oBiletService.GetSessionAsync(request);
            if (!sessionResponse.IsSuccess)
            {
                return new GeneralResponse<SessionResponseDto>();
            }

            SessionResponseDto response = sessionResponse.Data.Data.Adapt<SessionResponseDto>();
            return new GeneralResponse<SessionResponseDto>(response);
        }

        public async Task<GeneralResponse<BusLocationsResponseDto[]>> GetBusLocationsAsync(GeneralRequestDto<BusLocationRequestDto> model)
        {
            GetBusLocationsRequest request = model.RequestItem.Adapt<GetBusLocationsRequest>();
            request.DeviceSession = (await GetTokenFromSession(model.SessionRequest)).Adapt<DeviceSession>();

            var buslocationsResponse = await _oBiletService.GetBusLocationsAsync(request);
            if (!buslocationsResponse.IsSuccess)
            {
                return new GeneralResponse<BusLocationsResponseDto[]>();
            }

            return buslocationsResponse.Data.Adapt<GeneralResponse<BusLocationsResponseDto[]>>();
        }

        public async Task<GeneralResponse<BusJourneysResponseDto[]>> GetJourneysAsync(GeneralRequestDto<BusJourneysRequestDto> model)
        {
            GetBusJourneysRequest request = new GetBusJourneysRequest();
            request.Data = model.RequestItem.Adapt<GetBusJourneysRequestData>();
            request.DeviceSession = (await GetTokenFromSession(model.SessionRequest)).Adapt<DeviceSession>();

            var journeysResponse = await _oBiletService.GetJourneysAsync(request);
            if (!journeysResponse.IsSuccess)
            {
                return new GeneralResponse<BusJourneysResponseDto[]>();
            }

            GeneralResponse<BusJourneysResponseDto[]> response = journeysResponse.Data.Adapt<GeneralResponse<BusJourneysResponseDto[]>>();

            return response;
        }
    }
}
