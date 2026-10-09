using System.Text.Json.Serialization;

namespace backend.Models.DTOs
{
    public class OpenFoodFactsSearchResponse
    {
        [JsonPropertyName("products")]
        public List<OpenFoodFactsProduct>? Products { get; set; }
    }
}
