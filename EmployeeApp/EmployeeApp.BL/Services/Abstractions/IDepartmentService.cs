using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Services.Abstractions;

public interface IDepartmentService
{
    Task<ICollection<Department>> GetAllAsync();
    Task<Department> GetByIdAsync(int id);
    Task<Department> AddAsync(DepartmentCreateDto departmentCreateDto);
    Task UpdateAsync(Department department);
    void Delete(Department department);
}