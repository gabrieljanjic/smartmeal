namespace backend.Models.DTOs
{
    public class CreateProductAndAddToShoppingCartDto
    {
        public string Name { get; set; }
        public string? Brand { get; set; }
        public string EanCode { get; set; }
        public int Amount { get; set; }
        public double? Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
