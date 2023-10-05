using OBilet.Integration.Services.Model.Base;
using OBilet.Integration.Services.Model.Request;
using OBilet.Integration.Services.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Abstract
{
    public interface IOBiletService
    {
        Task<GeneralResponse<GetSessionResponse>> GetSessionAsync(GetSessionRequest request);
        Task<GeneralResponse<GetBusLocationsResponse>> GetBusLocationsAsync(GetBusLocationsRequest request);
        Task<GeneralResponse<GetBusJourneysResponse>> GetJourneysAsync(GetBusJourneysRequest request);
    }
}
