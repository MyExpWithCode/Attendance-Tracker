using AttendanceService.Business.Interfaces;
using AttendanceService.DAL.Models;
using AttendanceService.DAL.Repositories;

namespace AttendanceService.Business.Services;

public class AttendanceBusiness : IAttendanceBusiness
{
    private readonly IAttendanceRepository _repository;

    public AttendanceBusiness(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<AttendanceRecord> CheckInAsync(string employeeId, string employeeName)
    {
        var record = new AttendanceRecord
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            EmployeeName = employeeName,
            CheckInTime = DateTime.UtcNow,
            Status = DateTime.UtcNow.Hour > 9 ? "Late" : "Present"
        };

        await _repository.AddAsync(record);
        return record;
    }

    public async Task<AttendanceRecord?> CheckOutAsync(string employeeId)
    {
        var record = await _repository.GetActiveByEmployeeIdAsync(employeeId);
        if (record == null)
            return null;

        record.CheckOutTime = DateTime.UtcNow;

        var hoursWorked = (record.CheckOutTime.Value - record.CheckInTime).TotalHours;
        if (hoursWorked < 4)
            record.Status = "HalfDay";

        await _repository.UpdateAsync(record);
        return record;
    }

    public async Task<IEnumerable<AttendanceRecord>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(string employeeId)
    {
        return await _repository.GetByEmployeeIdAsync(employeeId);
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByDateAsync(DateTime date)
    {
        return await _repository.GetByDateAsync(date);
    }
}
