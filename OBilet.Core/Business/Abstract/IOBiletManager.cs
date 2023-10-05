using OBilet.Integration.Services.Model.Base;
using OBilet.Integration.Services.Model.Request;
using OBilet.Integration.Services.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.Business.Abstract
{
    public interface IOBiletManager
    {
        Task<GeneralResponse<GetSessionResponse>> GetSessionAsync(GetSessionRequest request);
        Task<GeneralResponse<GetBusLocationsRequest>> GetBusLocationsAsync(GetBusLocationsRequest request);
        Task<GeneralResponse<GetBusJourneysResponse>> GetJourneysAsync(GetBusJourneysRequest request);
    }
}
