using CareerLync.DTOs;
using CareerLync.Models;
using CareerLync.Interfaces;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CareerLync.Controllers
{
    [Authorize(Roles = "Employer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerRepository _employerRepo;

        public EmployerController(IEmployerRepository employerRepo)
        {
            _employerRepo = employerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employers = await _employerRepo.GetAllEmployersAsync();
            return Ok(employers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employer = await _employerRepo.GetEmployerByIdAsync(id);
            if (employer == null) return NotFound();
            return Ok(employer);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployerDto dto)
        {
            var created = await _employerRepo.AddEmployerAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EmployerId }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployerDto employerDto)
        {
            
            if (id != employerDto.EmployerId) return BadRequest();
            var updated = await _employerRepo.UpdateEmployerAsync(id,employerDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _employerRepo.DeleteEmployerAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

    }
}
