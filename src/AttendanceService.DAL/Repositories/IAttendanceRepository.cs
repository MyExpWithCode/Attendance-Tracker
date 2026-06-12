using AttendanceService.DAL.Models;

namespace AttendanceService.DAL.Repositories;

public interface IAttendanceRepository
{
    Task<int> AddAsync(AttendanceRecord record);
    Task<int> UpdateAsync(AttendanceRecord record);
    Task<AttendanceRecord?> GetByIdAsync(Guid id);
    Task<AttendanceRecord?> GetActiveByEmployeeIdAsync(string employeeId);
    Task<IEnumerable<AttendanceRecord>> GetAllAsync();
    Task<IEnumerable<AttendanceRecord>> GetByDateAsync(DateTime date);
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(string employeeId);
}
