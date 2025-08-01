
using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace CareerLync.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly AppDbContext _context;

        public EmployerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployerDto>> GetAllEmployersAsync()
        {
            return await _context.Employers
                .Where(e => !e.IsDeleted)
                .Select(e => new EmployerDto
                {
                    EmployerId = e.EmployerId,
                    UserId = e.UserId,
                    CompanyName = e.CompanyName,
                    CompanyWebsite = e.CompanyWebsite
                })
                .ToListAsync();
        }

        public async Task<EmployerDto> GetEmployerByIdAsync(int id)
        {
            var employer = await _context.Employers
                .FirstOrDefaultAsync(e => e.EmployerId == id && !e.IsDeleted);

            if (employer == null) return null;

            return new EmployerDto
            {
                EmployerId = employer.EmployerId,
                UserId = employer.UserId,
                CompanyName = employer.CompanyName,
                CompanyWebsite = employer.CompanyWebsite
            };
        }


        public async Task<EmployerDto> AddEmployerAsync(EmployerDto dto)
        {
            var employer = new Employers
            {
                UserId = dto.UserId,
                CompanyName = dto.CompanyName,
                CompanyWebsite = dto.CompanyWebsite
            };

            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();

            dto.EmployerId = employer.EmployerId;
            return dto;
        }

        public async Task<EmployerDto> UpdateEmployerAsync(int id, EmployerDto dto)
        {
            var existing = await _context.Employers.FirstOrDefaultAsync(e => e.EmployerId == id && !e.IsDeleted);
            if (existing == null) return null;

            existing.CompanyName = dto.CompanyName;
            existing.CompanyWebsite = dto.CompanyWebsite;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteEmployerAsync(int id)
        {
            var employer = await _context.Employers.FindAsync(id);
            if (employer == null ||  employer.IsDeleted) return false;

            employer.IsDeleted = true; //soft delete flag update
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
