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
    private readonly IDepartmentService _service;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _service = departmentService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var departments = await _service.GetAllAsync();
            return Ok(departments);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var department = await _service.GetByIdAsync(id);
            return Ok(department);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] DepartmentCreateDto departmentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var department = await _service.AddAsync(departmentCreateDto);
            return Ok(department);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _service.UpdateAsync(id, departmentUpdateDto);
            return result ? Ok("Update successful") : BadRequest("Update failed");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        try
        {
            var result = await _service.SoftDeleteAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Restore/{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        try
        {
            var result = await _service.RestoreAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
