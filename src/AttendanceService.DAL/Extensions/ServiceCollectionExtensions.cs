using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Repositories;
using AttendanceService.DAL.TypeHandlers;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceService.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddSingleton<IDbConnectionFactory>(sp =>
            new DbConnectionFactory(configuration.GetConnectionString("AttendanceDbConnectionString")!));
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
