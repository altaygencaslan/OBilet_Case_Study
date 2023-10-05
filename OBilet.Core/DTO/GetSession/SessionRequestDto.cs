using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetSession
{
    public class SessionRequestDto
    {
        public Connection Connection { get; set; }
        public Browser Browser { get; set; }
    }

    public class Connection
    {
        public string IpAddress { get; set; }
        public string Port { get; set; }
    }

    public class Browser
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
