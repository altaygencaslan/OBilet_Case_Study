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

namespace OBilet.Integration.Services.Concrete
{
    public class OBiletService : IOBiletService
    {
        private readonly string baseUrl;
        private readonly IApiHelper _apiHelper;

        public OBiletService(IApiHelper apiHelper)
        {
            baseUrl = Endpoints.BaseApiUrl;
            _apiHelper = apiHelper;
        }

        public async Task<GeneralResponse<GetSessionResponse>> GetSessionAsync(GetSessionRequest request)
        {
            return await _apiHelper.PostAsync<GetSessionResponse, GetSessionRequest>(baseUrl, Endpoints.GetSessionEndPoint, request);
        }

        public async Task<GeneralResponse<GetBusLocationsResponse>> GetBusLocationsAsync(GetBusLocationsRequest request)
        {
            return await _apiHelper.PostAsync<GetBusLocationsResponse, GetBusLocationsRequest>(baseUrl, Endpoints.GetBusLocationsEndPoint, request);
        }

        public async Task<GeneralResponse<GetBusJourneysResponse>> GetJourneysAsync(GetBusJourneysRequest request)
        {
            return await _apiHelper.PostAsync<GetBusJourneysResponse, GetBusJourneysRequest>(baseUrl, Endpoints.GetBusJourneysEndPoint, request);
        }
    }
}
