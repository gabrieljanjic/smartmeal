namespace backend.Models.DTOs
{
    public class CreateMealItemDto
    {
        public int MealNumber { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }
    }
}