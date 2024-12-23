using EmployeeApp.BL.DTOs.EmployeeDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Services.Abstractions;

public interface IEmployeeService
{
    Task<ICollection<EmployeeReadDto>> GetAllAsync();
    Task<EmployeeReadDto> GetByIdAsync(int id);
    Task<EmployeeReadDto> AddAsync(EmployeeCreateDto employeeCreateDto);
    Task<bool> UpdateAsync(int id, EmployeeUpdateDto employeeUpdateDto);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> RestoreAsync(int id);
    Task<bool> DeleteAsync(int id);
}