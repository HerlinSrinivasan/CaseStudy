using CareerLync.DTOs;
using CareerLync.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareerLync.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobListingRepository _jobApplicationRepo;

        public JobApplicationController(IJobListingRepository jobAoolicationRepo)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string title, [FromQuery] string location)
        {
            var jobs = await _jobApplicationRepo.GetAllAsync(title, location);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobApplicationRepo.GetByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobListingDto dto)
        {
            var created = await _jobApplicationRepo.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.JobListingId, created });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobListingDto dto)
        {
            if (id != dto.JobListingId) return BadRequest();
            var updated = await _jobApplicationRepo.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jobApplicationRepo.DeleteJobListingAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
