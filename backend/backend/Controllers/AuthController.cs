using backend.Data;
using backend.Models.Domain;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbAPIContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(DbAPIContext context,IConfiguration configuration,ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(new { name,role });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly=true,
                Secure=true,
                SameSite=SameSiteMode.None
            });

            return Ok(new { message = "Logged out" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if(existingUser != null)
            {
                return BadRequest("User already exisist");
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Role="User",
                MealsPerDay = 3,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            SetTokenCookie(token);

            return Ok(new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
            }
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user =await _context.Users.FirstOrDefaultAsync(u=>u.Email==loginDto.Email);
            
            if(user==null)
            {
                return Unauthorized("Wrong email or password");
            }

            var result=_passwordHasher.VerifyHashedPassword(user,user.PasswordHash, loginDto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Wrong email or password");
            }

            var token = GenerateJwtToken(user);
            SetTokenCookie(token);

            return Ok(new AuthResponseDto
            {
                Name=user.Name,
                Email=user.Email
            });
        }

        private void SetTokenCookie(string token)
        {
            var expiryMinutes = double.Parse(_configuration["Jwt:ExpiryMinutes"]);

            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly=true,
                Secure=true,
                SameSite=SameSiteMode.None,
                Expires=DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryMinutes = double.Parse(_configuration["JWT:ExpiryMinutes"]);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
