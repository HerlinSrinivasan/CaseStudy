
using CareerLync.DTOs;
using CareerLync.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLync.Interfaces
{
    public interface IJobSeekerRepository
    {
        Task<IEnumerable<JobSeekerDto>> GetAllJobSeekersAsync();
        Task<JobSeekerDto> GetJobSeekerByIdAsync(int id);
        Task<JobSeekerDto> AddJobSeekerAsync(JobSeekerDto jobSeekerDto);
        Task<JobSeekerDto> UpdateJobSeekerAsync(int id, JobSeekerDto jobSeekerDto);
        Task<bool> DeleteJobSeekerAsync(int id);
    }
}
