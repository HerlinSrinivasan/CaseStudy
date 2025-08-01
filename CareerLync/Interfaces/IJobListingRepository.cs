using CareerLync.DTOs;
using CareerLync.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CareerLync.Interfaces
{
    public interface IJobListingRepository
    {
        Task<IEnumerable<JobListingDto>> GetAllAsync(string title = null, string location = null);
        Task<JobListingDto> GetByIdAsync(int id);
        Task<JobListingDto> AddAsync(JobListingDto jobDto);
        Task<JobListingDto> UpdateAsync(int id, JobListingDto jobDto);
        Task<bool> DeleteJobListingAsync(int id);
    
    }
}
