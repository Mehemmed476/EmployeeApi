using AutoMapper;
using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Exceptions;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.BL.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentReadDto> AddAsync(DepartmentCreateDto departmentCreateDto)
    {
        var department = _mapper.Map<Department>(departmentCreateDto);
        department.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _departmentRepository.AddAsync(department);
        await _departmentRepository.SaveChangesAsync();

        return _mapper.Map<DepartmentReadDto>(department);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Department not found");

        _departmentRepository.Delete(department);
        await _departmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<DepartmentReadDto>> GetAllAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return _mapper.Map<ICollection<DepartmentReadDto>>(departments);
    }

    public async Task<DepartmentReadDto> GetByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Department not found");

        return _mapper.Map<DepartmentReadDto>(department);
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Department not found");

        if (department.DeletedAt == null)
            throw new InvalidOperationException("Department is not soft deleted");

        department.DeletedAt = null;
        _departmentRepository.Restore(department);
        await _departmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Department not found");

        if (department.DeletedAt != null)
            throw new InvalidOperationException("Department is already soft deleted");

        department.DeletedAt = DateTime.UtcNow.AddHours(4);
        _departmentRepository.SoftDelete(department);
        await _departmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int id, DepartmentUpdateDto departmentUpdateDto)
    {
        var existingDepartment = await _departmentRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Department not found");

        _mapper.Map(departmentUpdateDto, existingDepartment);
        existingDepartment.ModifiedAt = DateTime.UtcNow.AddHours(4);

        await _departmentRepository.UpdateAsync(existingDepartment);
        await _departmentRepository.SaveChangesAsync();

        return true;
    }
}
