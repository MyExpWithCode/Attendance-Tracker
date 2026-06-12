using AttendanceService.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AttendanceSvc = AttendanceService.Business.Services.AttendanceService;
using EmployeeSvc = AttendanceService.Business.Services.EmployeeService;

namespace AttendanceService.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IAttendanceService, AttendanceSvc>();
        services.AddScoped<IEmployeeService, EmployeeSvc>();

        return services;
    }
}
