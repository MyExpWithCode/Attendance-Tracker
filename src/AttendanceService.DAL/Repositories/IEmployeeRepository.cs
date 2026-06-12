using AttendanceService.DAL.Models;

namespace AttendanceService.DAL.Repositories;

public interface IEmployeeRepository
{
    Task<int> AddAsync(Employee employee);
    Task<int> UpdateAsync(Employee employee);
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByCodeAsync(string employeeCode);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetByDepartmentAsync(string department);
    Task<int> DeleteAsync(Guid id);
}
