using CareerLync.DTOs;
using CareerLync.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerLync.Repositories
{
    public class ResumeRepository : IResumeRepository
    {

        private readonly AppDbContext _context;

        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResumeDto>> GetAllResumesAsync()
        {
            return await _context.Resumes
                .Where(r => !r.IsDeleted)
                .Select(r => new ResumeDto
                {
                    ResumeId = r.ResumeId,
                    JobSeekerId = r.JobSeekerId,
                    FilePath = r.FilePath
                }).ToListAsync();
        }
        public async Task<ResumeDto> GetResumeByIdAsync(int id)
        {
            var resume = await _context.Resumes
                .Where(r => r.ResumeId == id && !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (resume == null) return null;

            return new ResumeDto
            {
                ResumeId = resume.ResumeId,
                JobSeekerId = resume.JobSeekerId,
                FilePath = resume.FilePath
            };
        }
               public async Task<ResumeDto> AddResumeAsync(ResumeDto dto)
        {
            var newResume = new Resumes
            {
                JobSeekerId = dto.JobSeekerId,
                FilePath = dto.FilePath
            };

            _context.Resumes.Add(newResume);
            await _context.SaveChangesAsync();

            return new ResumeDto
            {
                ResumeId = newResume.ResumeId,
                JobSeekerId = newResume.JobSeekerId,
                FilePath = newResume.FilePath
            };
        }
        public async Task<ResumeDto> UpdateResumeAsync(int id, ResumeDto dto)
        {
            var existing = await _context.Resumes.FirstOrDefaultAsync(r => r.ResumeId == id && !r.IsDeleted);
            if (existing == null) return null;

            existing.FilePath = dto.FilePath;
            existing.JobSeekerId = dto.JobSeekerId;

            await _context.SaveChangesAsync();

            return new ResumeDto
            {
                ResumeId = existing.ResumeId,
                JobSeekerId = existing.JobSeekerId,
                FilePath = existing.FilePath
            };
        }

        public async Task<bool> DeleteResumeAsync(int id)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.ResumeId == id && !r.IsDeleted);
            if (resume == null) return false;

            resume.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
