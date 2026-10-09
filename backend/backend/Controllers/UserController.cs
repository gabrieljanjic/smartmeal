using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/toggle-admin")]
        public async Task<IActionResult> ToggleAdmin(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            if (userId == id)
                return BadRequest("Ne možeš promijeniti vlastitu ulogu.");

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userRepository.ToggleAdminAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            if (userId == id)
                return BadRequest("Ne možeš obrisati samog sebe.");

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userRepository.DeleteAsync(user);
            return Ok();
        }
    }
}