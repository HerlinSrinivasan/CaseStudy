using CareerLync.DTOs.Auth;
using CareerLync.Interfaces;

using CareerLync.Models;
using System.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CareerLync.Repositories;
using System.CodeDom.Compiler;
using Microsoft.IdentityModel.Tokens;


namespace CareerLync.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _config;

        public AuthRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (exists)
            {
                return new AuthResponseDto { Message = "User Already Exists", IsSuccess = false };

            }

            var newUser = new Users
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = GenerateToken(newUser);

            return new AuthResponseDto
            {
                Email = newUser.Email,
                Role = newUser.Role,
                Token = token,
                IsSuccess = true,
                Message = "Registration successful"
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            if (user == null)
            {
                return new AuthResponseDto { Message = "Invalid credentials", IsSuccess = false };
            }
            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role,
                Token = token,
                IsSuccess = true,
                Message = "Login Successfull"
            };
        }

        private string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt: Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

