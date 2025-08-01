
using CareerLync.DTOs;


namespace CareerLync.Interfaces
{
    public interface IEmployerRepository
    {

        Task<IEnumerable<EmployerDto>> GetAllEmployersAsync();
        Task<EmployerDto> GetEmployerByIdAsync(int id);
        Task<EmployerDto> AddEmployerAsync(EmployerDto employerDto);
        Task<EmployerDto> UpdateEmployerAsync(int id, EmployerDto employerDto);
        Task<bool> DeleteEmployerAsync(int id);

    }
}
