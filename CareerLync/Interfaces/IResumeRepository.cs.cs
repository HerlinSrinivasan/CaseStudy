using CareerLync.DTOs;
using CareerLync.Models;


namespace CareerLync.Repositories
{
    public interface IResumeRepository
    {
        Task<IEnumerable<ResumeDto>> GetAllResumesAsync();
        Task<ResumeDto> GetResumeByIdAsync(int id);
        Task<ResumeDto> AddResumeAsync(ResumeDto dto);
        Task<ResumeDto> UpdateResumeAsync(int id, ResumeDto dto);
        Task<bool> DeleteResumeAsync(int id);
    }
}
