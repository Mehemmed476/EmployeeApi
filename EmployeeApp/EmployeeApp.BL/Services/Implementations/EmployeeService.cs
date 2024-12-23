using AutoMapper;
using EmployeeApp.BL.DTOs.EmployeeDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Exceptions;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.BL.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeReadDto> AddAsync(EmployeeCreateDto employeeCreateDto)
    {
        var employee = _mapper.Map<Employee>(employeeCreateDto);
        employee.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync();

        return _mapper.Map<EmployeeReadDto>(employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Employee not found");

        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<EmployeeReadDto>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return _mapper.Map<ICollection<EmployeeReadDto>>(employees);
    }

    public async Task<EmployeeReadDto> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Employee not found");

        return _mapper.Map<EmployeeReadDto>(employee);
    }

    public async Task<bool> RestoreAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Employee not found");

        if (employee.DeletedAt == null)
            throw new InvalidOperationException("Employee is not soft deleted");

        employee.DeletedAt = null;
        _employeeRepository.Restore(employee);
        await _employeeRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Employee not found");

        if (employee.DeletedAt != null)
            throw new InvalidOperationException("Employee is already soft deleted");

        employee.DeletedAt = DateTime.UtcNow.AddHours(4);
        _employeeRepository.SoftDelete(employee);
        await _employeeRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        var existingEmployee = await _employeeRepository.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Employee not found");

        _mapper.Map(employeeUpdateDto, existingEmployee);
        existingEmployee.ModifiedAt = DateTime.UtcNow.AddHours(4);

        await _employeeRepository.UpdateAsync(existingEmployee);
        await _employeeRepository.SaveChangesAsync();

        return true;
    }
}