using AttendanceService.Business.Interfaces;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Queries;
using AttendanceService.DAL.Repositories;
using AttendanceService.DAL.TypeHandlers;
using Dapper;
using AttendanceSvc = AttendanceService.Business.Services.AttendanceService;
using EmployeeSvc = AttendanceService.Business.Services.EmployeeService;

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDbConnectionFactory>(sp =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceSvc>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeSvc>();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure tables exist
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
    using var connection = dbFactory.CreateConnection();
    await connection.ExecuteAsync("DROP TABLE IF EXISTS attendance_records");
    await connection.ExecuteAsync(EmployeeQueries.CreateTable);
    await connection.ExecuteAsync(AttendanceQueries.CreateTable);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Attendance Service API v1"));
}

app.MapControllers();
app.Run();
