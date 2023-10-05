using OBilet.Integration.Services.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Request
{
    public class GetBusJourneysRequest : BaseApiRequest<GetBusJourneysRequestData>
    {
    }

    public class GetBusJourneysRequestData
    {
        [JsonPropertyName("origin-id")]
        public int OriginId { get; set; }

        [JsonPropertyName("destination-id")]
        public int DestinationId { get; set; }

        [JsonPropertyName("departure-date")]
        public DateTime DepartureDate { get; set; } //Dokümanda INT olarak belirtiliyor
    }
}
