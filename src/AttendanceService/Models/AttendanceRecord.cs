namespace AttendanceService.Models;

public class AttendanceRecord
{
    public Guid Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; }
}

public enum AttendanceStatus
{
    Present,
    Absent,
    Late,
    HalfDay
}
