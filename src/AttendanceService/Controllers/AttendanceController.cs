using AttendanceService.Business.Interfaces;
using AttendanceService.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceBusiness _attendanceBusiness;

    public AttendanceController(IAttendanceBusiness attendanceBusiness)
    {
        _attendanceBusiness = attendanceBusiness;
    }

    [HttpPost("checkin")]
    public async Task<ActionResult<AttendanceRecord>> CheckIn([FromBody] CheckInRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.EmployeeId))
            return BadRequest("EmployeeId is required.");

        var record = await _attendanceBusiness.CheckInAsync(request.EmployeeId, request.EmployeeName);
        return Ok(record);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<AttendanceRecord>> CheckOut([FromBody] CheckOutRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.EmployeeId))
            return BadRequest("EmployeeId is required.");

        var record = await _attendanceBusiness.CheckOutAsync(request.EmployeeId);
        if (record == null)
            return NotFound("No active check-in found for this employee.");

        return Ok(record);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetAll()
    {
        return Ok(await _attendanceBusiness.GetAllAsync());
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetByEmployee(string employeeId)
    {
        var records = await _attendanceBusiness.GetByEmployeeIdAsync(employeeId);
        return Ok(records);
    }

    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<AttendanceRecord>>> GetByDate(DateTime date)
    {
        return Ok(await _attendanceBusiness.GetByDateAsync(date));
    }
}

public class CheckInRequest
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
}

public class CheckOutRequest
{
    public string EmployeeId { get; set; } = string.Empty;
}
