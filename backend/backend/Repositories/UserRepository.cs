using backend.Data;
using backend.Models.DTOs;
using backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbAPIContext _context;

        public UserRepository(DbAPIContext context)
        {
            _context = context;
        }

        public async Task<List<UsersReponseDTO>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UsersReponseDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task ToggleAdminAsync(User user)
        {
            user.Role = user.Role == "Admin" ? "User" : "Admin";
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}