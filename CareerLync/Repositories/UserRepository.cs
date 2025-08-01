using CareerLync.DTOs;
using CareerLync.Interfaces;
using CareerLync.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CareerLync.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.Where(u => !u.IsDeleted).Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role,

            }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }
        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = new Users
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Role = userDto.Role,
                Password = "default"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.UserId = user.UserId;
            return userDto;
        }

        public async Task<UserDto> UpdateUserAsync(int id ,UserDto userDto)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return null;

            existing.UserName = userDto.UserName;
            existing.Email = userDto.Email;
            existing.Role = userDto.Role;
            await _context.SaveChangesAsync();
            return userDto;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
    
