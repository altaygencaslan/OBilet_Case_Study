using Microsoft.AspNetCore.Mvc;
using OBilet.Core.Business.Abstract;
using OBilet.Core.DTO.GetSession;
using System;
using System.Text.Json;
using UAParser;

namespace OBilet.Web.Controllers
{
    public class BaseController : Controller
    {
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

    }
}
