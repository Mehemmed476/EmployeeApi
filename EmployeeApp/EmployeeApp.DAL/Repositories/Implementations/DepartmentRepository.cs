using EmployeeApp.Core.Entities;
using EmployeeApp.DAL.Contexts;
using EmployeeApp.DAL.Repositories.Abstractions;

namespace EmployeeApp.DAL.Repositories.Implementations;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context) : base(context)
    {
    }
}