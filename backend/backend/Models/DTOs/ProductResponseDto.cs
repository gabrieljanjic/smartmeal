namespace backend.Models.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string EanCode { get; set; }
        public string Name { get; set; }
        public string? Brand { get; set; }
        public double? CaloriesPer100 { get; set; }
        public double? ProteinPer100 { get; set; }
        public double? FatPer100 { get; set; }
        public double? CarbsPer100 { get; set; }
        public string? ImageUrl { get; set; }
        public double? Quantity { get; set; }
        public string? Unit { get; set; }
    }
}