using Dapper;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Models;
using AttendanceService.DAL.Queries;

namespace AttendanceService.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EmployeeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> AddAsync(Employee employee)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(EmployeeQueries.Insert, employee);
    }

    public async Task<int> UpdateAsync(Employee employee)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(EmployeeQueries.Update, employee);
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Employee>(EmployeeQueries.GetById, new { Id = id });
    }

    public async Task<Employee?> GetByCodeAsync(string employeeCode)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Employee>(EmployeeQueries.GetByCode, new { EmployeeCode = employeeCode });
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Employee>(EmployeeQueries.GetAll);
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Employee>(EmployeeQueries.GetByDepartment, new { Department = department });
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(EmployeeQueries.Delete, new { Id = id, UpdatedAt = DateTime.UtcNow });
    }
}
