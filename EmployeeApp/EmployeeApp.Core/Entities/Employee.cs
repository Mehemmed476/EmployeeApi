using EmployeeApp.Core.Entities.Base;

namespace EmployeeApp.Core.Entities;

public class Employee : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}