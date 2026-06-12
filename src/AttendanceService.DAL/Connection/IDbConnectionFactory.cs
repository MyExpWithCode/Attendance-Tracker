using System.Data;

namespace AttendanceService.DAL.Connection;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
