using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services
{
    public class Endpoints
    {
        public static string BaseApiUrl => "https://v2-api.obilet.com/";
        public static string GetSessionEndPoint => "api/client/getsession/";
        public static string GetBusLocationsEndPoint => "api/location/getbuslocations";
        public static string GetBusJourneysEndPoint => "api/journey/getbusjourneys";
    }
}
