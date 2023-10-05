using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Request
{
    public class GetSessionRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; } = 7; //POSTMAN'de 1 görünüyor.

        [JsonPropertyName("connection")]
        public Connection Connection { get; set; }

        [JsonPropertyName("browser")]
        public Browser Browser { get; set; }

        [JsonPropertyName("application")]
        public Application Application { get; set; } //POSTMAN'de bu kısım yok.
    }

    public class Connection
    {
        [JsonPropertyName("ip-address")]
        public string IpAddress { get; set; }

        [JsonPropertyName("port")]
        public string Port { get; set; }
    }

    public class Browser
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class Application
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0.0.0";

        [JsonPropertyName("equipment-id")]
        public string EquipmentId { get; set; }
    }
}
