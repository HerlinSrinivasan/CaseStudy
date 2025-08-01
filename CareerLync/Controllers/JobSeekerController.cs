
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareerLync.Models;
using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CareerLync.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerRepository _jobSeekerRepo;

        public JobSeekerController(IJobSeekerRepository jobSeekerRepo)
        {
            _jobSeekerRepo = jobSeekerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobSeekers = await _jobSeekerRepo.GetAllJobSeekersAsync();
            return Ok(jobSeekers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobSeeker = await _jobSeekerRepo.GetJobSeekerByIdAsync(id);
            if (jobSeeker == null) return NotFound();
            return Ok(jobSeeker);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobSeekerDto jobSeekerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _jobSeekerRepo.AddJobSeekerAsync(jobSeekerDto);
            return CreatedAtAction(nameof(GetById), new { id = created.JobSeekerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobSeekerDto jobSeekerdto)
        {
            if (id != jobSeekerdto.JobSeekerId) return BadRequest();
            var updated = await _jobSeekerRepo.UpdateJobSeekerAsync(id, jobSeekerdto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jobSeekerRepo.DeleteJobSeekerAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
