using System.Collections.Concurrent;
using AttendanceService.Models;

namespace AttendanceService.Services;

public class AttendanceServiceImpl : IAttendanceService
{
    private readonly ConcurrentDictionary<Guid, AttendanceRecord> _records = new();

    public AttendanceRecord CheckIn(CheckInRequest request)
    {
        var record = new AttendanceRecord
        {
            Id = Guid.NewGuid(),
            EmployeeId = request.EmployeeId,
            EmployeeName = request.EmployeeName,
            CheckInTime = DateTime.UtcNow,
            Status = DateTime.UtcNow.Hour > 9 ? AttendanceStatus.Late : AttendanceStatus.Present
        };

        _records[record.Id] = record;
        return record;
    }

    public AttendanceRecord? CheckOut(CheckOutRequest request)
    {
        var record = _records.Values
            .FirstOrDefault(r => r.EmployeeId == request.EmployeeId && r.CheckOutTime == null);

        if (record == null)
            return null;

        record.CheckOutTime = DateTime.UtcNow;

        var hoursWorked = (record.CheckOutTime.Value - record.CheckInTime).TotalHours;
        if (hoursWorked < 4)
            record.Status = AttendanceStatus.HalfDay;

        return record;
    }

    public IEnumerable<AttendanceRecord> GetAllRecords()
    {
        return _records.Values.OrderByDescending(r => r.CheckInTime);
    }

    public AttendanceRecord? GetByEmployeeId(string employeeId)
    {
        return _records.Values
            .Where(r => r.EmployeeId == employeeId)
            .OrderByDescending(r => r.CheckInTime)
            .FirstOrDefault();
    }

    public IEnumerable<AttendanceRecord> GetByDate(DateTime date)
    {
        return _records.Values
            .Where(r => r.CheckInTime.Date == date.Date)
            .OrderByDescending(r => r.CheckInTime);
    }
}
