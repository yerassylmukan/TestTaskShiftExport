using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Validators;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(AppContext.BaseDirectory, "work_schedule.db");
var connectionString = $"Data Source={dbPath};";

builder.Services.AddDbContext<WorkScheduleContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers()
    .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<EmployeeDtoValidator>(); });

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IShiftService, ShiftService>();

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();