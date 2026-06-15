using AttendanceService.Business.Extensions;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Extensions;
using AttendanceService.DAL.Queries;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddDataAccessLayer(builder.Configuration, builder.Environment);
builder.Services.AddBusinessLayer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure tables exist
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
    using var connection = dbFactory.CreateConnection();
    await connection.ExecuteAsync(EmployeeQueries.CreateTable);
    await connection.ExecuteAsync(AttendanceQueries.CreateTable);
}

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Attendance Service API v1"));

app.MapControllers();
app.Run();