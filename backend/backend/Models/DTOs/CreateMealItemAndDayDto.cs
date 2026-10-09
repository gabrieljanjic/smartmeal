namespace backend.Models.DTOs
{
    public class CreateMealItemAndDayDto
    {
        public string Date { get; set; }

        public string Name { get;set; }
        public string? Brand { get; set; }

        public string EanCode { get; set; }

        public int MealNumber { get; set; }

        public int Amount { get; set; }
        public double? Quantity { get; set; }
        public string? Unit { get; set; }
    }
}