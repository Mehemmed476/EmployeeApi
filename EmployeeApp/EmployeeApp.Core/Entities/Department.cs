using EmployeeApp.Core.Entities.Base;

namespace EmployeeApp.Core.Entities;

public class Department : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public ICollection<Employee>? Employees { get; set; }
}