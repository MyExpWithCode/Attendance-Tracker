namespace AttendanceService.Models;

public class CheckInRequest
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
}

public class CheckOutRequest
{
    public string EmployeeId { get; set; } = string.Empty;
}
