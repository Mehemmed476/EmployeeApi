using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Contexts;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.DAL.Repositories.Implementations;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }
}