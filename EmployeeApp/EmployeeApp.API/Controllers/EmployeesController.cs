using EmployeeApp.BL.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
   private readonly IEmployeeService _employeeService;

   public EmployeesController(IEmployeeService employeeService)
   {
      _employeeService = employeeService;
   }
}

