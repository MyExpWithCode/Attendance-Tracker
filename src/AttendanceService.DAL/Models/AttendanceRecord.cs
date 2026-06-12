namespace AttendanceService.DAL.Models;

public class AttendanceRecord
{
    public Guid Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty; // populated via JOIN, not stored
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string Status { get; set; } = string.Empty;
}
