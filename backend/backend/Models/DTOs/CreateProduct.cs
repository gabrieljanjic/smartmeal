namespace backend.Models.Domain
{
    public class CreateProduct
    {
        public string EanCode { get; set; }

        public string Name { get; set; }

        public string? Brand { get; set; }

        public decimal? CaloriesPer100 { get; set; }

        public decimal? ProteinPer100 { get; set; }

        public decimal? FatPer100 { get; set; }

        public decimal? CarbsPer100 { get; set; }

        public string? ImageUrl { get; set; }
    }
}
