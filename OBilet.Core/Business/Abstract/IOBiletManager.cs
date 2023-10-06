using OBilet.Core.DTO.Base;
using OBilet.Core.DTO.GetBusJourneys;
using OBilet.Core.DTO.GetBusLocations;
using OBilet.Core.DTO.GetSession;
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
        Task<GeneralResponse<SessionResponseDto>> GetSessionAsync(SessionRequestDto model);
        Task<GeneralResponse<BusLocationsResponseDto>> GetBusLocationsAsync(GeneralRequestDto<BusLocationRequestDto> model);
        Task<GeneralResponse<BusJourneysResponseDto>> GetJourneysAsync(GeneralRequestDto<BusJourneysRequestDto> model);
    }
}
