namespace backend.Models.DTOs
{
    public class MealItemResponseDto
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public int MealNumber { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
