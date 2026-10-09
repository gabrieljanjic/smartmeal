namespace backend.Models.Domain
{
    public class MealItem
    {
        public int Id { get; set; }

        public int DayId { get; set; }

        public Day Day { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int MealNumber { get; set; }

        public int Amount { get; set; }

        public bool isBought { get; set; }
    }
}