using backend.Models.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace backend.Services
{
    public class CijenePriceService : ICijenePriceService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CijenePriceService(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<List<CijeneChain>> GetPricesAsync(string eanCode)
        {
            var baseUrl = _configuration["CijeneApi:BaseUrl"];
            var apiKey = _configuration["CijeneApi:ApiKey"];

            if (string.IsNullOrWhiteSpace(baseUrl) ||
                string.IsNullOrWhiteSpace(apiKey))
            {
                return new List<CijeneChain>();
            }

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var url =
                $"{baseUrl.TrimEnd('/')}/v1/products/{Uri.EscapeDataString(eanCode)}/" +
                "?chains=konzum,kaufland,lidl";

            try
            {
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new List<CijeneChain>();

                var body = await response.Content.ReadAsStringAsync();

                var product =
                    JsonSerializer.Deserialize<CijeneProductResponse>(
                        body,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return product?.Chains ?? new List<CijeneChain>();
            }
            catch
            {
                return new List<CijeneChain>();
            }
        }

        public async Task<(bool Success, int StatusCode, string Body)>
            SearchProductsAsync(string query)
        {
            var baseUrl = _configuration["CijeneApi:BaseUrl"];
            var apiKey = _configuration["CijeneApi:ApiKey"];

            if (string.IsNullOrWhiteSpace(baseUrl) ||
                string.IsNullOrWhiteSpace(apiKey))
            {
                return (false, 500, "Cijene API nije konfiguriran."
                );
            }

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var url =
                $"{baseUrl.TrimEnd('/')}/v1/products/" +
                "?chains=konzum,kaufland,lidl" +
                $"&q={Uri.EscapeDataString(query)}" +
                "&fuzzy=true" +
                "&limit=50";

            try
            {
                var response = await _client.GetAsync(url);

                var body = await response.Content.ReadAsStringAsync();

                return (
                    response.IsSuccessStatusCode,
                    (int)response.StatusCode,
                    body
                );
            }
            catch
            {
                return (false, 500, "Greška prilikom komunikacije s Cijene API-jem.");
            }
        }
    }
}