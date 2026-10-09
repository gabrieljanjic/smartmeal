namespace backend.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";  
        public int MealsPerDay { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Day> Days { get; set; } = new();
    }
}
