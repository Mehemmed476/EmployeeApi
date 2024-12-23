using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Services.Abstractions;

public interface IDepartmentService
{
    Task<ICollection<DepartmentReadDto>> GetAllAsync();
    Task<DepartmentReadDto> GetByIdAsync(int id);
    Task<DepartmentReadDto> AddAsync(DepartmentCreateDto departmentCreateDto);
    Task<bool> UpdateAsync(int id, DepartmentUpdateDto departmentUpdateDto);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> RestoreAsync(int id);
    Task<bool> DeleteAsync(int id);
}