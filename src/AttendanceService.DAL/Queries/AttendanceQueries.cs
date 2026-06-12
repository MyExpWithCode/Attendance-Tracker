namespace AttendanceService.DAL.Queries;

public static class AttendanceQueries
{
    public const string Insert = @"
        INSERT INTO attendance_records (id, employee_id, employee_name, check_in_time, check_out_time, status)
        VALUES (@Id, @EmployeeId, @EmployeeName, @CheckInTime, @CheckOutTime, @Status)";

    public const string Update = @"
        UPDATE attendance_records 
        SET check_out_time = @CheckOutTime, status = @Status
        WHERE id = @Id";

    public const string GetById = @"
        SELECT id AS Id, employee_id AS EmployeeId, employee_name AS EmployeeName, 
               check_in_time AS CheckInTime, check_out_time AS CheckOutTime, status AS Status
        FROM attendance_records 
        WHERE id = @Id";

    public const string GetActiveByEmployeeId = @"
        SELECT id AS Id, employee_id AS EmployeeId, employee_name AS EmployeeName, 
               check_in_time AS CheckInTime, check_out_time AS CheckOutTime, status AS Status
        FROM attendance_records 
        WHERE employee_id = @EmployeeId AND check_out_time IS NULL
        ORDER BY check_in_time DESC
        LIMIT 1";

    public const string GetAll = @"
        SELECT id AS Id, employee_id AS EmployeeId, employee_name AS EmployeeName, 
               check_in_time AS CheckInTime, check_out_time AS CheckOutTime, status AS Status
        FROM attendance_records 
        ORDER BY check_in_time DESC";

    public const string GetByDate = @"
        SELECT id AS Id, employee_id AS EmployeeId, employee_name AS EmployeeName, 
               check_in_time AS CheckInTime, check_out_time AS CheckOutTime, status AS Status
        FROM attendance_records 
        WHERE check_in_time::date = @Date
        ORDER BY check_in_time DESC";

    public const string GetByEmployeeId = @"
        SELECT id AS Id, employee_id AS EmployeeId, employee_name AS EmployeeName, 
               check_in_time AS CheckInTime, check_out_time AS CheckOutTime, status AS Status
        FROM attendance_records 
        WHERE employee_id = @EmployeeId
        ORDER BY check_in_time DESC";

    public const string CreateTable = @"
        CREATE TABLE IF NOT EXISTS attendance_records (
            id UUID PRIMARY KEY,
            employee_id VARCHAR(50) NOT NULL,
            employee_name VARCHAR(200) NOT NULL,
            check_in_time TIMESTAMP NOT NULL,
            check_out_time TIMESTAMP NULL,
            status VARCHAR(20) NOT NULL
        )";
}
