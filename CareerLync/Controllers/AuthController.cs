using Microsoft.AspNetCore.Http;
using CareerLync.DTOs;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CareerLync.Interfaces;
using CareerLync.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;

namespace CareerLync.Controllers

{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
            {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authRepo.RegisterAsync(registerDto);

            if (!result.IsSuccess) { return BadRequest(result); }

            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authRepo.LoginAsync(loginDto);
            if (!result.IsSuccess) { return Unauthorized(result); }

            return Ok(result);
        }
    }

}
