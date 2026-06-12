using AttendanceService.Models;

namespace AttendanceService.Services;

public interface IAttendanceService
{
    AttendanceRecord CheckIn(CheckInRequest request);
    AttendanceRecord? CheckOut(CheckOutRequest request);
    IEnumerable<AttendanceRecord> GetAllRecords();
    AttendanceRecord? GetByEmployeeId(string employeeId);
    IEnumerable<AttendanceRecord> GetByDate(DateTime date);
}
