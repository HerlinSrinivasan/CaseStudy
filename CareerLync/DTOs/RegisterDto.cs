using System.ComponentModel.DataAnnotations;

namespace CareerLync.DTOs.Auth
{
    public class RegisterDto

    {
        [Required]
        public string UserName{ get; set; }

        [Required,EmailAddress]
        public string Email {  get; set; }

        [Required, MinLength(6)]
        public string Password{ get; set; }
        [Required]
        public string Role {  get; set; }

    }
}
