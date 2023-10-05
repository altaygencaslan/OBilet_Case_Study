using Microsoft.Extensions.Logging;
using OBilet.Integration.Services.Abstract;
using OBilet.Integration.Services.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Concrete
{
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiHelper> _logger;

        public ApiHelper(HttpClient httpClient, ILogger<ApiHelper> logger)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
            _logger = logger;
        }

        public async Task<GeneralResponse<T>> PostAsync<T, K>(string baseUrl, string endpointUrl, K data)
        {
            try
            {
                _logger.LogInformation($"Post start with Request {JsonSerializer.Serialize(data)}");

                _httpClient.BaseAddress = new Uri(baseUrl);

                var serializedObject = JsonSerializer.Serialize(data);
                var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpointUrl, content);
                var responseSTR = await response.Content.ReadAsStringAsync();

                T responseData = JsonSerializer.Deserialize<T>(responseSTR);

                _logger.LogInformation($"Post end with Request {JsonSerializer.Serialize(data)}, Response: {JsonSerializer.Serialize(responseData)}");
                return new GeneralResponse<T>(responseData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post get error with Request {JsonSerializer.Serialize(data)}, Error: {JsonSerializer.Serialize(ex)}");
                return new GeneralResponse<T>();
            }
        }
    }
}
