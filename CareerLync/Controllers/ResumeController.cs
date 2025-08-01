using CareerLync.DTOs;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerLync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepo;

        public ResumeController(IResumeRepository resumeRepo)
        {
            _resumeRepo = resumeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resumes = await _resumeRepo.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resume = await _resumeRepo.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();
            return Ok(resume);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResumeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _resumeRepo.AddResumeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ResumeId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ResumeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _resumeRepo.UpdateResumeAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _resumeRepo.DeleteResumeAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
