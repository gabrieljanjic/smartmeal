using backend.Models.Domain;

namespace backend.Models.DTOs
{
    public class UsersReponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
