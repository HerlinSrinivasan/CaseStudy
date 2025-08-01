using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CareerLync.Repositories
{
    public class JobListingRepository : IJobListingRepository
    {
        private readonly AppDbContext _context;

        public JobListingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobListingDto>> GetAllAsync(string title = null, string location = null)
        {
            var query = _context.JobListings
                .Where(j => !j.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(j => j.Title.Contains(title));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(j => j.Location.Contains(location));

            return await query.Select(j => new JobListingDto
            {
                JobListingId = j.JobListingId,
                EmployerId = j.EmployerId,
                Title = j.Title,
                Description = j.Description,
                Location = j.Location
            }).ToListAsync();
        }
        public async Task<JobListingDto> GetByIdAsync(int id)
        {
            var job = await _context.JobListings.Where(j => j.JobListingId == id && !j.IsDeleted)
                                    .FirstOrDefaultAsync();
            if (job == null) return null;

            return new JobListingDto
            {
                JobListingId = job.JobListingId,
                EmployerId = job.EmployerId,
                Title = job.Title,
                Description = job.Description,
                Location = job.Location
            };



        }

        public async Task<JobListingDto> AddAsync(JobListingDto dto)
        {
            var job = new JobListings
            {
                EmployerId = dto.EmployerId,
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location
            };

            _context.JobListings.Add(job);
            await _context.SaveChangesAsync();

            dto.JobListingId = job.JobListingId;
            dto.JobListingId = job.JobListingId;
            return dto;

        }
        public async Task<JobListingDto> UpdateAsync(int id, JobListingDto dto)
        {
            var existing = await _context.JobListings.FirstOrDefaultAsync(j => j.JobListingId == id && !j.IsDeleted);
            if (existing == null) return null;  

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Location = dto.Location;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteJobListingAsync(int id)
        {
            var job = await _context.JobListings.FindAsync(id);
            if (job == null || job.IsDeleted) return false;

            job.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
