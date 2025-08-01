using CareerLync.DTOs;

namespace CareerLync.Interfaces
{
    public interface IJobApplicationRepository
    {

        Task<IEnumerable<JobApplicationDto>> GetAllApplicationsAsync();
        Task<JobApplicationDto> GetApplicationByIdAsync(int id);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId);
        Task<JobApplicationDto> CreateApplicationAsync(JobApplicationDto applicationDto);
        Task<JobApplicationDto> UpdateApplicationStatusAsync(int id, string status);
        Task<bool> DeleteApplicationAsync(int id); // Soft delete
    }
}
