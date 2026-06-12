namespace AttendanceService.DAL.Queries;

public static class EmployeeQueries
{
    public const string Insert = @"
        INSERT INTO employees (id, employee_code, first_name, last_name, email, department, designation, joining_date, is_active, created_at, updated_at)
        VALUES (@Id, @EmployeeCode, @FirstName, @LastName, @Email, @Department, @Designation, @JoiningDate, @IsActive, @CreatedAt, @UpdatedAt)";

    public const string Update = @"
        UPDATE employees 
        SET first_name = @FirstName, last_name = @LastName, email = @Email, 
            department = @Department, designation = @Designation, 
            is_active = @IsActive, updated_at = @UpdatedAt
        WHERE id = @Id";

    public const string GetById = @"
        SELECT id AS Id, employee_code AS EmployeeCode, first_name AS FirstName, last_name AS LastName,
               email AS Email, department AS Department, designation AS Designation,
               joining_date AS JoiningDate, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt
        FROM employees 
        WHERE id = @Id";

    public const string GetByCode = @"
        SELECT id AS Id, employee_code AS EmployeeCode, first_name AS FirstName, last_name AS LastName,
               email AS Email, department AS Department, designation AS Designation,
               joining_date AS JoiningDate, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt
        FROM employees 
        WHERE employee_code = @EmployeeCode";

    public const string GetAll = @"
        SELECT id AS Id, employee_code AS EmployeeCode, first_name AS FirstName, last_name AS LastName,
               email AS Email, department AS Department, designation AS Designation,
               joining_date AS JoiningDate, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt
        FROM employees 
        ORDER BY created_at DESC";

    public const string GetByDepartment = @"
        SELECT id AS Id, employee_code AS EmployeeCode, first_name AS FirstName, last_name AS LastName,
               email AS Email, department AS Department, designation AS Designation,
               joining_date AS JoiningDate, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt
        FROM employees 
        WHERE department = @Department
        ORDER BY first_name";

    public const string Delete = @"
        UPDATE employees SET is_active = false, updated_at = @UpdatedAt WHERE id = @Id";

    public const string CreateTable = @"
        CREATE TABLE IF NOT EXISTS employees (
            id UUID PRIMARY KEY,
            employee_code VARCHAR(20) NOT NULL UNIQUE,
            first_name VARCHAR(100) NOT NULL,
            last_name VARCHAR(100) NOT NULL,
            email VARCHAR(200) NOT NULL UNIQUE,
            department VARCHAR(100) NOT NULL,
            designation VARCHAR(100) NOT NULL,
            joining_date DATE NOT NULL,
            is_active BOOLEAN NOT NULL DEFAULT true,
            created_at TIMESTAMP NOT NULL,
            updated_at TIMESTAMP NULL
        )";
}
