using OBilet.Integration.Services.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Response
{
    public class GetBusLocationsResponse : BaseApiResponse<GetBusLocationsResponseData>
    {
    }

    public class GetBusLocationsResponseData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("parent-id")]
        public int ParentId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("geo-location")]
        public GeoLocation GeoLocation { get; set; }

        [JsonPropertyName("zoom")]
        public int Zoom { get; set; }

        [JsonPropertyName("tz-code")]
        public string TzCode { get; set; }

        [JsonPropertyName("weather-code")]
        public string WeatherCode { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("reference-code")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }

    }

    public class GeoLocation
    {
        [JsonPropertyName("")]
        public double Latitude { get; set; }

        [JsonPropertyName("")]
        public double Longitude { get; set; }

        [JsonPropertyName("")]
        public int Zoom { get; set; }
    }
}
