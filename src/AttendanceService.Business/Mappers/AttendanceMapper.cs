using AttendanceService.DAL.Models;

namespace AttendanceService.Business.Mappers;

public static class AttendanceMapper
{
    public static AttendanceRecord ToNewCheckIn(string employeeId)
    {
        return new AttendanceRecord
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            CheckInTime = DateTime.UtcNow,
            Status = DateTime.UtcNow.Hour > 9 ? "Late" : "Present"
        };
    }

    public static void ApplyCheckOut(AttendanceRecord record)
    {
        record.CheckOutTime = DateTime.UtcNow;

        var hoursWorked = (record.CheckOutTime.Value - record.CheckInTime).TotalHours;
        if (hoursWorked < 4)
            record.Status = "HalfDay";
    }
}
