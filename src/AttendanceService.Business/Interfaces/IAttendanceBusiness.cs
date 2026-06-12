using AttendanceService.DAL.Models;

namespace AttendanceService.Business.Interfaces;

public interface IAttendanceBusiness
{
    Task<AttendanceRecord> CheckInAsync(string employeeId, string employeeName);
    Task<AttendanceRecord?> CheckOutAsync(string employeeId);
    Task<IEnumerable<AttendanceRecord>> GetAllAsync();
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(string employeeId);
    Task<IEnumerable<AttendanceRecord>> GetByDateAsync(DateTime date);
}
