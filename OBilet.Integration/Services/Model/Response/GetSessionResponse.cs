using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Response
{
    public class GetSessionResponse
    {
        [JsonPropertyName("success")]
        public string Success { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("user-message")]
        public string UserMessage { get; set; }

        [JsonPropertyName("api-request-id")]
        public string ApiRequestId { get; set; }

        [JsonPropertyName("controller")]
        public string Controller { get; set; }


        //Dokümanda aşağıdaki alanlar yok:
        [JsonPropertyName("client-request-id")]
        public string ClientRequestId { get; set; }

        [JsonPropertyName("web-correlation-id")]
        public string WebCorrelationId { get; set; }

        [JsonPropertyName("correlation-id")]
        public string CorrelationId { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }

        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }



        //Dokümanda aşağıdaki alanlar yok:

        [JsonPropertyName("affiliate")]
        public string Affiliate { get; set; }

        [JsonPropertyName("device-type")]
        public int DeviceType { get; set; }

        [JsonPropertyName("device")]
        public string Device { get; set; }

        [JsonPropertyName("ip-country")]
        public string IpCountry { get; set; }

    }
}
