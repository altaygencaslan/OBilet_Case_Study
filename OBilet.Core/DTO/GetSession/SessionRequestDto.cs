using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetSession
{
    public class SessionRequestDto
    {
        public ConnectionDto Connection { get; set; }
        public BrowserDto Browser { get; set; }
    }

    public class ConnectionDto
    {
        public string IpAddress { get; set; }
        public string Port { get; set; }
    }

    public class BrowserDto
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
