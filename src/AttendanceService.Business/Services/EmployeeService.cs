using AttendanceService.Business.Interfaces;
using AttendanceService.Business.Mappers;
using AttendanceService.DAL.Models;
using AttendanceService.DAL.Repositories;

namespace AttendanceService.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee> CreateAsync(string employeeCode, string firstName, string lastName, string email, string department, string designation, DateOnly joiningDate)
    {
        var employee = EmployeeMapper.ToNewEmployee(employeeCode, firstName, lastName, email, department, designation, joiningDate);

        await _repository.AddAsync(employee);
        return employee;
    }

    public async Task<Employee?> UpdateAsync(Guid id, string firstName, string lastName, string email, string department, string designation)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
            return null;

        EmployeeMapper.ApplyUpdate(employee, firstName, lastName, email, department, designation);

        await _repository.UpdateAsync(employee);
        return employee;
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Employee?> GetByCodeAsync(string employeeCode)
    {
        return await _repository.GetByCodeAsync(employeeCode);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department)
    {
        return await _repository.GetByDepartmentAsync(department);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _repository.DeleteAsync(id);
        return result > 0;
    }
}
