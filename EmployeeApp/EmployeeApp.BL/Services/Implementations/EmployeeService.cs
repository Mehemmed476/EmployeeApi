using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.BL.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }


    public async Task<ICollection<Employee>> GetAllAsync()
    {
        ICollection<Employee> employees = await _employeeRepository.GetAllAsync();
        if (employees == null)
        {
            throw new ApplicationException("Employees not found");
        }
        
        return employees;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        Employee employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new KeyNotFoundException();
        }
        
        return employee;
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        
        await _employeeRepository.UpdateAsync(employee);
        await _employeeRepository.SaveChangesAsync();
    }

    public async void Delete(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        
        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync();
    } 
}