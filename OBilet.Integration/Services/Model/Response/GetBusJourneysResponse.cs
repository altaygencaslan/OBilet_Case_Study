using OBilet.Integration.Services.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Response
{
    public class GetBusJourneysResponse : BaseApiResponse<GetBusJourneysResponseData[]>
    {
    }

    public class GetBusJourneysResponseData
    {
        //[JsonPropertyName("available-seats")]
        //public int AvailableSeats { get; set; }

        //[JsonPropertyName("bus-type")]
        //public string BusType { get; set; }

        //[JsonPropertyName("cancellation-offset")]
        //public int? CancellationOffset { get; set; }

        //[JsonPropertyName("destination-location")]
        //public string DestinationLocation { get; set; }

        //[JsonPropertyName("destination-location-id")]
        //public int DestinationLocationId { get; set; }

        //[JsonPropertyName("disable-sales-without-gov-id")]
        //public bool DisableSalesWithoutGovId { get; set; }

        //[JsonPropertyName("display-offset")]
        //public TimeSpan DisplayOffset { get; set; }

        //[JsonPropertyName("features")]
        //public Feature[] Features { get; set; }

        //[JsonPropertyName("has-bus-shuttle")]
        //public bool HasBusShuttle { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        //[JsonPropertyName("is-active")]
        //public bool IsActive { get; set; }

        //[JsonPropertyName("is-promoted")]
        //public bool IsPromoted { get; set; }

        [JsonPropertyName("journey")]
        public Journey Journey { get; set; }

        //[JsonPropertyName("origin-location")]
        //public string OriginLocation { get; set; }

        //[JsonPropertyName("origin-location-id")]
        //public int OriginLocationId { get; set; }

        [JsonPropertyName("partner-id")]
        public int PartnerId { get; set; }

        [JsonPropertyName("partner-name")]
        public string PartnerName { get; set; }

        //[JsonPropertyName("partner-rating")]
        //public decimal? PartnerRating { get; set; }

        //[JsonPropertyName("route-id")]
        //public int RouteId { get; set; }

        //[JsonPropertyName("total-seats")]
        //public int TotalSeats { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }

    public class Feature
    {
        [JsonPropertyName("back-color")]
        public string BackColor { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fore-color")]
        public string ForeColor { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("is-promoted")]
        public bool IsPromoted { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }
    }

    public class Journey
    {
        [JsonPropertyName("arrival")]
        public DateTime Arrival { get; set; }

        //[JsonPropertyName("available")]
        //public string Available { get; set; }

        //[JsonPropertyName("booking")]
        //public string Booking { get; set; }

        //[JsonPropertyName("bus-name")]
        //public string BusName { get; set; }

        //[JsonPropertyName("code")]
        //public string Code { get; set; }

        //[JsonPropertyName("currency")]
        //public string Currency { get; set; }

        [JsonPropertyName("departure")]
        public DateTime Departure { get; set; }

        //[JsonPropertyName("description")]
        //public string Description { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        //[JsonPropertyName("duration")]
        //public TimeSpan Duration { get; set; }

        //[JsonPropertyName("features")]
        //public string[] Features { get; set; }

        [JsonPropertyName("internet-price")]
        public decimal InternetPrice { get; set; }

        //[JsonPropertyName("kind")]
        //public string Kind { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("original-price")]
        public decimal OriginalPrice { get; set; }

        //[JsonPropertyName("policy")]
        //public Policy Policy { get; set; }

        //[JsonPropertyName("stops")]
        //public Stop[] Stops { get; set; }
    }

    public class Policy
    {
        [JsonPropertyName("gov-id")]
        public bool GovId { get; set; }

        [JsonPropertyName("lht")]
        public bool Lht { get; set; }

        [JsonPropertyName("max-seats")]
        public int? MaxSeats { get; set; }

        [JsonPropertyName("max-single")]
        public int? MaxSingle { get; set; }

        [JsonPropertyName("max-single-females")]
        public int? MaxSingleFemales { get; set; }

        [JsonPropertyName("max-single-males")]
        public int? MaxSingleMales { get; set; }

        [JsonPropertyName("mixed-genders")]
        public bool MixedGenders { get; set; }
    }

    public class Stop
    {
        [JsonPropertyName("is-destination")]
        public bool IsDestination { get; set; }

        [JsonPropertyName("is-origin")]
        public bool IsOrigin { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("station")]
        public string Station { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
