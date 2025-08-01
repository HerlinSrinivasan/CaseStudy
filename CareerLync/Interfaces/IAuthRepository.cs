using System.Threading.Tasks;
using CareerLync.DTOs.Auth;
namespace CareerLync.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto>LoginAsync(LoginDto loginDto);
    }
}
