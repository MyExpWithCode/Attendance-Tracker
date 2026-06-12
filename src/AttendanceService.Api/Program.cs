using AttendanceService.Business.Extensions;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Extensions;
using AttendanceService.DAL.Queries;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLayer();
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
