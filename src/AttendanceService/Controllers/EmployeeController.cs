using AttendanceService.Business.Interfaces;
using AttendanceService.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeBusiness;

    public EmployeeController(IEmployeeService employeeBusiness)
    {
        _employeeBusiness = employeeBusiness;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.EmployeeCode))
            return BadRequest("EmployeeCode is required.");

        var joiningDate = DateOnly.FromDateTime(request.JoiningDate);
        var employee = await _employeeBusiness.CreateAsync(
            request.EmployeeCode, request.FirstName, request.LastName,
            request.Email, request.Department, request.Designation, joiningDate);

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> Update(Guid id, [FromBody] UpdateEmployeeRequest request)
    {
        var employee = await _employeeBusiness.UpdateAsync(
            id, request.FirstName, request.LastName, request.Email, request.Department, request.Designation);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(Guid id)
    {
        var employee = await _employeeBusiness.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<Employee>> GetByCode(string code)
    {
        var employee = await _employeeBusiness.GetByCodeAsync(code);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        return Ok(await _employeeBusiness.GetAllAsync());
    }

    [HttpGet("department/{department}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetByDepartment(string department)
    {
        return Ok(await _employeeBusiness.GetByDepartmentAsync(department));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _employeeBusiness.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}

public class CreateEmployeeRequest
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public DateTime JoiningDate { get; set; }
}

public class UpdateEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
}
