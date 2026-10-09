namespace backend.Models.Domain
{
    public class Day
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateOnly Date { get; set; }

        public ICollection<MealItem> MealItems { get; set; } = new List<MealItem>();
    }
}