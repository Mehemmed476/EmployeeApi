using EmployeeApp.BL.DTOs.DepartmentDTOs;
using EmployeeApp.BL.Services.Abstractions;
using EmployeeApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
   private readonly IDepartmentService _departmentService;

   public DepartmentsController(IDepartmentService departmentService)
   {
      _departmentService = departmentService;
   }
   
   [HttpGet]
   public async Task<ICollection<Department>> GetDepartments()
   {
      return await _departmentService.GetAllAsync();
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<Department>> GetDepartment(int id)
   {
      Department department = await _departmentService.GetByIdAsync(id);
      
      return department;
   }

   [HttpPost]
   public async Task<ActionResult<DepartmentCreateDto>> PostDepartment(DepartmentCreateDto departmentCreateDto)
   {
      await _departmentService.AddAsync(departmentCreateDto);
      return departmentCreateDto;
   }
}

