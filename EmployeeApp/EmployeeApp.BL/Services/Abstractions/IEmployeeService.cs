using EmployeeApp.Core.Entities;

namespace EmployeeApp.BL.Services.Abstractions;

public interface IEmployeeService
{
    Task<ICollection<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    void Delete(Employee employee); 
}