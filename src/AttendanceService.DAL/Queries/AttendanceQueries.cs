namespace AttendanceService.DAL.Queries;

public static class AttendanceQueries
{
    public const string Insert = @"
        INSERT INTO attendance_records (id, employee_id, check_in_time, check_out_time, status)
        VALUES (@Id, @EmployeeId, @CheckInTime, @CheckOutTime, @Status)";

    public const string Update = @"
        UPDATE attendance_records 
        SET check_out_time = @CheckOutTime, status = @Status
        WHERE id = @Id";

    public const string GetById = @"
        SELECT a.id AS Id, a.employee_id AS EmployeeId, 
               CONCAT(e.first_name, ' ', e.last_name) AS EmployeeName,
               a.check_in_time AS CheckInTime, a.check_out_time AS CheckOutTime, a.status AS Status
        FROM attendance_records a
        LEFT JOIN employees e ON e.employee_code = a.employee_id
        WHERE a.id = @Id";

    public const string GetActiveByEmployeeId = @"
        SELECT a.id AS Id, a.employee_id AS EmployeeId,
               CONCAT(e.first_name, ' ', e.last_name) AS EmployeeName,
               a.check_in_time AS CheckInTime, a.check_out_time AS CheckOutTime, a.status AS Status
        FROM attendance_records a
        LEFT JOIN employees e ON e.employee_code = a.employee_id
        WHERE a.employee_id = @EmployeeId AND a.check_out_time IS NULL
        ORDER BY a.check_in_time DESC
        LIMIT 1";

    public const string GetAll = @"
        SELECT a.id AS Id, a.employee_id AS EmployeeId,
               CONCAT(e.first_name, ' ', e.last_name) AS EmployeeName,
               a.check_in_time AS CheckInTime, a.check_out_time AS CheckOutTime, a.status AS Status
        FROM attendance_records a
        LEFT JOIN employees e ON e.employee_code = a.employee_id
        ORDER BY a.check_in_time DESC";

    public const string GetByDate = @"
        SELECT a.id AS Id, a.employee_id AS EmployeeId,
               CONCAT(e.first_name, ' ', e.last_name) AS EmployeeName,
               a.check_in_time AS CheckInTime, a.check_out_time AS CheckOutTime, a.status AS Status
        FROM attendance_records a
        LEFT JOIN employees e ON e.employee_code = a.employee_id
        WHERE a.check_in_time::date = @Date
        ORDER BY a.check_in_time DESC";

    public const string GetByEmployeeId = @"
        SELECT a.id AS Id, a.employee_id AS EmployeeId,
               CONCAT(e.first_name, ' ', e.last_name) AS EmployeeName,
               a.check_in_time AS CheckInTime, a.check_out_time AS CheckOutTime, a.status AS Status
        FROM attendance_records a
        LEFT JOIN employees e ON e.employee_code = a.employee_id
        WHERE a.employee_id = @EmployeeId
        ORDER BY a.check_in_time DESC";

    public const string CreateTable = @"
        CREATE TABLE IF NOT EXISTS attendance_records (
            id UUID PRIMARY KEY,
            employee_id VARCHAR(50) NOT NULL,
            check_in_time TIMESTAMP NOT NULL,
            check_out_time TIMESTAMP NULL,
            status VARCHAR(20) NOT NULL
        )";
}
