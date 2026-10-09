using System.Text.Json.Serialization;
using static backend.Controllers.ShoppingCartController;

namespace backend.Models.DTOs
{
    public class CijeneProductResponse
    {
        [JsonPropertyName("ean")]
        public string? Ean { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }

        [JsonPropertyName("chains")]
        public List<CijeneChain>? Chains { get; set; }
    }
}