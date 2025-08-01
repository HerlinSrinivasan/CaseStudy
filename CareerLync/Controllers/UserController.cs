using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareerLync.Models;
using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CareerLync.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserDto userDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _userRepo.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId) return BadRequest();
            var updated = await _userRepo.UpdateUserAsync(id,userDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userRepo.DeleteUserAsync(id);
            if (!deleted) return BadRequest();
            return NoContent();
        }
    }
}
