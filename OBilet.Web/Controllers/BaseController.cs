using Microsoft.AspNetCore.Mvc;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.GetSession;
using System.Text.Json;

namespace OBilet.Web.Controllers
{
    public class BaseController : Controller
    {

        private readonly IOBiletManager _oBiletManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IOBiletManager oBiletManager, IHttpContextAccessor contextAccessor)
        {
            _oBiletManager = oBiletManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<SessionResponseDto> GetTokenFromSession()
        {
            var tokenJSON = _contextAccessor.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(tokenJSON))
            {
                var obiletSession = await _oBiletManager.GetSessionAsync(new Core.DTO.GetSession.SessionRequestDto
                {
                    Connection = new Core.DTO.GetSession.ConnectionDto
                    {
                        IpAddress = "165.114.41.21",
                        Port = "5117",
                    },
                    Browser = new Core.DTO.GetSession.BrowserDto
                    {
                        Name = "Chrome",
                        Version = "47.0.0.12",
                    },
                });

                tokenJSON = JsonSerializer.Serialize(obiletSession.Data);
                _contextAccessor.HttpContext.Session.SetString("token", tokenJSON);
            }

            SessionResponseDto session = JsonSerializer.Deserialize<SessionResponseDto>(tokenJSON);
            return session;
        }

    }
}
