using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerLync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IJobApplicationRepository _applicationRepo;

        public ApplicationController(IJobApplicationRepository applicationRepo)
        {
            _applicationRepo = applicationRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetAllApplications()
        {
            var applications = await _applicationRepo.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employer,JobSeeker")]
        public async Task<ActionResult<JobApplicationDto>> GetApplicationById(int id)
        {
            var application = await _applicationRepo.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound("Application not found");

            return Ok(application);
        }

        [HttpGet("jobseeker/{jobSeekerId}")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetApplicationsByJobSeekerId(int jobSeekerId)
        {
            var applications = await _applicationRepo.GetApplicationsByJobSeekerIdAsync(jobSeekerId);
            return Ok(applications);
        }
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult<JobApplicationDto>> CreateApplication(JobApplicationDto dto)
        {
            var created = await _applicationRepo.CreateApplicationAsync(dto);
            return CreatedAtAction(nameof(GetApplicationById), new { id = created.ApplicationId }, created);
        }
    
     [HttpPut("{id}/status")]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult<JobApplicationDto>> UpdateApplicationStatus(int id, [FromBody] string status)
        {
            var updated = await _applicationRepo.UpdateApplicationStatusAsync(id, status);
            if (updated == null)
                return NotFound("Application not found or inactive");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var result = await _applicationRepo.DeleteApplicationAsync(id);
            if (!result)
                return NotFound("Application not found or already inactive");

            return NoContent();
        }
    }

}
