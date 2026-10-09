using backend.Models.DTOs;
using backend.Models.Domain;

namespace backend.Repositories
{
    public interface IUserRepository
    {
        Task<List<UsersReponseDTO>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task ToggleAdminAsync(User user);
        Task DeleteAsync(User user);
    }
}