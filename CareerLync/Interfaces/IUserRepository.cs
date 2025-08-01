

using CareerLync.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CareerLync.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> UpdateUserAsync(int id ,UserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
