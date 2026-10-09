using backend.Models.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backend.Services
{
    public class OpenFoodFactsService : IOpenFoodFactsService
    {
        private readonly HttpClient _client;

        public OpenFoodFactsService(HttpClient client, ILogger<OpenFoodFactsService> logger)
        {
            _client = client;
        }

        public async Task<OpenFoodFactsProduct?> GetByEanAsync(string eanCode)
        {
            try
            {
                var url = $"https://world.openfoodfacts.org/api/v2/product/{eanCode}.json";
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();

                var offResponse = JsonSerializer.Deserialize<OpenFoodFactsResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return offResponse?.Product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OpenFoodFactsProduct?> SearchByNameAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                var url = $"https://search.openfoodfacts.org/search?q={Uri.EscapeDataString(name)}";
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();

                var searchResponse = JsonSerializer.Deserialize<OpenFoodFactsSearchResponse>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return searchResponse?.Products?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class OpenFoodFactsSearchResponse
        {
            [JsonPropertyName("products")]
            public List<OpenFoodFactsProduct>? Products { get; set; }
        }
    }
}