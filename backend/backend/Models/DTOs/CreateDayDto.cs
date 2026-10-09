namespace backend.Models.DTOs
{
    public class CreateDayDto
    {
        public int UserId { get; set; }
        public DateOnly Date { get; set; }
    }
}
