using AttendanceService.Business.Interfaces;
using AttendanceService.Business.Mappers;
using AttendanceService.DAL.Models;
using AttendanceService.DAL.Repositories;

namespace AttendanceService.Business.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repository;

    public AttendanceService(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<AttendanceRecord> CheckInAsync(string employeeId)
    {
        var record = AttendanceMapper.ToNewCheckIn(employeeId);

        await _repository.AddAsync(record);
        return record;
    }

    public async Task<AttendanceRecord?> CheckOutAsync(string employeeId)
    {
        var record = await _repository.GetActiveByEmployeeIdAsync(employeeId);
        if (record == null)
            return null;

        AttendanceMapper.ApplyCheckOut(record);

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
