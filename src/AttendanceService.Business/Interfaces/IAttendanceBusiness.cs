using AttendanceService.DAL.Models;

namespace AttendanceService.Business.Interfaces;

public interface IAttendanceService
{
    Task<AttendanceRecord> CheckInAsync(string employeeId);
    Task<AttendanceRecord?> CheckOutAsync(string employeeId);
    Task<IEnumerable<AttendanceRecord>> GetAllAsync();
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(string employeeId);
    Task<IEnumerable<AttendanceRecord>> GetByDateAsync(DateTime date);
}
