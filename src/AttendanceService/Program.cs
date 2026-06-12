using AttendanceService.Business.Interfaces;
using AttendanceService.Business.Services;
using AttendanceService.DAL.Connection;
using AttendanceService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDbConnectionFactory>(sp =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")!));
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceBusiness, AttendanceBusiness>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
