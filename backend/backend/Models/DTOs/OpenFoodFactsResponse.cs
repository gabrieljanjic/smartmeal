using System.Text.Json.Serialization;

namespace backend.Models.DTOs
{
    public class OpenFoodFactsResponse
    {
        [JsonPropertyName("product")]
        public OpenFoodFactsProduct? Product { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }

    public class OpenFoodFactsProduct
    {
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("brands")]
        public string? Brands { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("nutriments")]
        public OpenFoodFactsNutriments? Nutriments { get; set; }

        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }

        [JsonPropertyName("quantity_unit")]
        public string? QuantityUnit { get; set; }
    }

    public class OpenFoodFactsNutriments
    {
        [JsonPropertyName("energy-kcal_100g")]
        public double? EnergyKcal100g { get; set; }

        [JsonPropertyName("proteins_100g")]
        public double? Proteins100g { get; set; }

        [JsonPropertyName("fat_100g")]
        public double? Fat100g { get; set; }

        [JsonPropertyName("carbohydrates_100g")]
        public double? Carbohydrates100g { get; set; }
    }
}