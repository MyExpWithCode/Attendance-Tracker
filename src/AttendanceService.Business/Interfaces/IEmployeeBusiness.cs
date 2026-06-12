using AttendanceService.DAL.Models;

namespace AttendanceService.Business.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateAsync(string employeeCode, string firstName, string lastName, string email, string department, string designation, DateOnly joiningDate);
    Task<Employee?> UpdateAsync(Guid id, string firstName, string lastName, string email, string department, string designation);
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByCodeAsync(string employeeCode);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetByDepartmentAsync(string department);
    Task<bool> DeleteAsync(Guid id);
}
