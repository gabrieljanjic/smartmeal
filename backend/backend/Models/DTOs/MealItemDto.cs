namespace backend.Models.DTOs
{
    public class MealItemDto
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public int MealNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsBought { get; set; }
        public ProductResponseDto Product { get; set; }
    }
}
