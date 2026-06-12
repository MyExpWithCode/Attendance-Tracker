using Dapper;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Models;
using AttendanceService.DAL.Queries;

namespace AttendanceService.DAL.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AttendanceRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> AddAsync(AttendanceRecord record)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(AttendanceQueries.Insert, record);
    }

    public async Task<int> UpdateAsync(AttendanceRecord record)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(AttendanceQueries.Update, record);
    }

    public async Task<AttendanceRecord?> GetByIdAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AttendanceRecord>(AttendanceQueries.GetById, new { Id = id });
    }

    public async Task<AttendanceRecord?> GetActiveByEmployeeIdAsync(string employeeId)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AttendanceRecord>(AttendanceQueries.GetActiveByEmployeeId, new { EmployeeId = employeeId });
    }

    public async Task<IEnumerable<AttendanceRecord>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<AttendanceRecord>(AttendanceQueries.GetAll);
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByDateAsync(DateTime date)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<AttendanceRecord>(AttendanceQueries.GetByDate, new { Date = date.Date });
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(string employeeId)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<AttendanceRecord>(AttendanceQueries.GetByEmployeeId, new { EmployeeId = employeeId });
    }
}
