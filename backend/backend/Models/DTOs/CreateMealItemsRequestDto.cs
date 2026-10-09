namespace backend.Models.DTOs
{
    public class CreateMealItemsRequestDto
    {
        public int UserId { get; set; }

        public DateOnly Date { get; set; }

        public List<CreateMealItemDto> Items { get; set; } = new();
    }
}