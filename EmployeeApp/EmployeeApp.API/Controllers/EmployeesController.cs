using EmployeeApp.BL.DTOs.EmployeeDTOs;
using EmployeeApp.BL.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService employeeService)
    {
        _service = employeeService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var employees = await _service.GetAllAsync();
            return Ok(employees);
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
            var employee = await _service.GetByIdAsync(id);
            return Ok(employee);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] EmployeeCreateDto employeeCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var employee = await _service.AddAsync(employeeCreateDto);
            return Ok(employee);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _service.UpdateAsync(id, employeeUpdateDto);
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


