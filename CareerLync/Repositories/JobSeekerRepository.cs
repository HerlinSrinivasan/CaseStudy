using System;
using CareerLync.Models;
using CareerLync.DTOs;
using CareerLync.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {

        private readonly AppDbContext _context;

        public JobSeekerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobSeekerDto>> GetAllJobSeekersAsync()
        {
            return await _context.JobSeekers
                .Where(js => !js.isDeleted)
                .Select(js => new JobSeekerDto
                {
                    JobSeekerId = js.JobSeekerId,
                    UserId = js.UserId,
                    PhoneNumber = js.PhoneNumber,
                    Summary = js.Summary
                }).ToListAsync();
        }

        public async Task<JobSeekerDto> GetJobSeekerByIdAsync(int id)
        {
            var js = await _context.JobSeekers
                .Where(js => js.JobSeekerId == id && !js.isDeleted)
                .FirstOrDefaultAsync();

            if (js == null) return null;

            return new JobSeekerDto
            {
                JobSeekerId = js.JobSeekerId,
                UserId = js.UserId,
                PhoneNumber = js.PhoneNumber,
                Summary = js.Summary
            };
        }
        public async Task<JobSeekerDto> AddJobSeekerAsync(JobSeekerDto dto)
        {
            var js = new JobSeekers
            {
                UserId = dto.UserId,
                PhoneNumber = dto.PhoneNumber,
                Summary = dto.Summary
            };

            _context.JobSeekers.Add(js);
            await _context.SaveChangesAsync();

            dto.JobSeekerId = js.JobSeekerId;
            return dto;
        }
        public async Task<JobSeekerDto> UpdateJobSeekerAsync(int id, JobSeekerDto dto)
        {
            var js = await _context.JobSeekers.FirstOrDefaultAsync(j => j.JobSeekerId == id && !j.isDeleted);
            if (js == null) return null;

            js.PhoneNumber = dto.PhoneNumber;
            js.Summary = dto.Summary;

            await _context.SaveChangesAsync();
            return dto;
        }
        public async Task<bool> DeleteJobSeekerAsync(int id)
        {
            var js = await _context.JobSeekers.FindAsync(id);
            if (js == null || js.isDeleted) return false;

            js.isDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
