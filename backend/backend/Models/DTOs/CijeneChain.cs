using System.Text.Json.Serialization;

namespace backend.Models.DTOs
{
    public class CijeneChain
    {
        [JsonPropertyName("chain")]
        public string? Chain { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }

        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }

        [JsonPropertyName("min_price")]
        public string? MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public string? MaxPrice { get; set; }

        [JsonPropertyName("avg_price")]
        public string? AvgPrice { get; set; }
    }
}