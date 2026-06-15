using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Repositories;
using AttendanceService.DAL.TypeHandlers;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AttendanceService.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        string connectionString;

        if (environment.IsProduction())
        {
            var host = configuration["DB_HOST"];
            var port = configuration["DB_PORT"];
            var database = configuration["DB_NAME"];
            var user = configuration["DB_USER"];
            var password = configuration["DB_PASSWORD"];
            connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password};";
        }
        else
        {
            connectionString = configuration.GetConnectionString("AttendanceDbConnectionString")!;
        }

        services.AddSingleton<IDbConnectionFactory>(sp =>
            new DbConnectionFactory(connectionString));
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
