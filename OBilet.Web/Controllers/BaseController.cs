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

        //public BaseController(IOBiletManager oBiletManager, IHttpContextAccessor contextAccessor)
        //{
        //    _oBiletManager = oBiletManager;
        //    _contextAccessor = contextAccessor;
        //}

        public SessionRequestDto GetSessionDefault()
        {
            return new SessionRequestDto
            {
                Connection = new ConnectionDto
                {
                    IpAddress = "165.114.41.21",
                    Port = "5117",
                },
                Browser = new BrowserDto
                {
                    Name = "Chrome",
                    Version = "47.0.0.12",
                },
            };
        }

    }
}
