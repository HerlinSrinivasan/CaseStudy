using System.Globalization;

namespace CareerLync.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        
        public bool IsSuccess {  get; set; }

        public string Message { get; set; }
    }
}
