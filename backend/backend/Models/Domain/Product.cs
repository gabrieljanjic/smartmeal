namespace backend.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string EanCode { get; set; }

        public string Name { get; set; }

        public string? Brand { get; set; }

        public double? Quantity { get; set; }

        public string? Unit { get; set; }

        public double? CaloriesPer100 { get; set; }

        public double? ProteinPer100 { get; set; }

        public double? FatPer100 { get; set; }

        public double? CarbsPer100 { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<MealItem> MealItems { get; set; } = new List<MealItem>();
    }
}