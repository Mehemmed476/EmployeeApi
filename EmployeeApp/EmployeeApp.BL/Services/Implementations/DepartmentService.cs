using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.BL.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }


    public async Task<ICollection<Department>> GetAllAsync()
    {
        ICollection<Department> departments = await _departmentRepository.GetAllAsync();
        if (departments == null)
        {
            throw new ApplicationException("Departments not found");
        }
        
        return departments;
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        Department department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            throw new KeyNotFoundException();
        }
        
        return department;
    }

    public async Task<Department> AddAsync(DepartmentCreateDto departmentCreateDto)
    {
        if (departmentCreateDto == null)
        {
            throw new ArgumentNullException(nameof(departmentCreateDto));
        }

        Department department = new Department()
        {
            Title = departmentCreateDto.Title,
            Description = departmentCreateDto.Description,
            CreatedBy = departmentCreateDto.CreatedBy,
            CreatedAt = DateTime.Now,
        };
        await _departmentRepository.AddAsync(department);
        await _departmentRepository.SaveChangesAsync();
        return department;
    }

    public async Task UpdateAsync(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(department));
        }
        
        await _departmentRepository.UpdateAsync(department);
        await _departmentRepository.SaveChangesAsync();
    }

    public async void Delete(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(department));
        }
        
        _departmentRepository.Delete(department);
        await _departmentRepository.SaveChangesAsync();
    }
}