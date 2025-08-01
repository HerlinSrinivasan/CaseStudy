using CareerLync.DTOs;
using CareerLync.Models;
using CareerLync.Interfaces;
using Microsoft.EntityFrameworkCore;
    
    namespace CareerLync.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _context;

        public JobApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync()
        {
            return await _context.JobApplications
                .Where(a => !a.IsDeleted)
                .Select(a => new JobApplicationDto
                {
                    ApplicationId = a.ApplicationId,
                    JobListingId = a.JobListingId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }
        public async Task<JobApplicationDto> GetApplicationByIdAsync(int id)
        {
            var application = await _context.JobApplications
                .Where(a => a.ApplicationId == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (application == null) return null;

            return new JobApplicationDto
            {
                ApplicationId = application.ApplicationId,
                JobListingId = application.JobListingId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };
        }
        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.JobApplications
                .Where(a => a.JobSeekerId == jobSeekerId && !a.IsDeleted)
                .Select(a => new JobApplicationDto
                {
                    ApplicationId = a.ApplicationId,
                    JobListingId = a.JobListingId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }
            

        public async Task<JobApplicationDto> CreateApplicationAsync(JobApplicationDto applicationDto)
        {
            var application = new JobApplications
            {
                JobListingId = applicationDto.JobListingId,
                JobSeekerId = applicationDto.JobSeekerId,
                Status = applicationDto.Status,
                AppliedAt = applicationDto.AppliedAt
            };

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            applicationDto.ApplicationId = application.ApplicationId;
            return applicationDto;
        }
        public async Task<JobApplicationDto> UpdateApplicationStatusAsync(int id, string status)
        {
            var application = await _context.JobApplications
                .Where(a => a.ApplicationId == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (application == null) return null;

            application.Status = status;
            await _context.SaveChangesAsync();

            return new JobApplicationDto
            {
                ApplicationId = application.ApplicationId,
                JobListingId = application.JobListingId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };

        }
        public async Task<bool> DeleteApplicationAsync(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if (application == null || application.IsDeleted) return false;

            application.IsDeleted = true; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
