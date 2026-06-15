using AttendanceService.DAL.Models;

namespace AttendanceService.Business.Mappers;

public static class EmployeeMapper
{
    public static Employee ToNewEmployee(string employeeCode, string firstName, string lastName, string email, string department, string designation, DateOnly joiningDate)
    {
        return new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeCode = employeeCode,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Department = department,
            Designation = designation,
            JoiningDate = joiningDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Employee employee, string firstName, string lastName, string email, string department, string designation)
    {
        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Email = email;
        employee.Department = department;
        employee.Designation = designation;
        employee.UpdatedAt = DateTime.UtcNow;
    }
}
