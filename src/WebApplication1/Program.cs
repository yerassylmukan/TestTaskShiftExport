using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbPath = Path.Combine(AppContext.BaseDirectory, "work_schedule.db");
var connectionString = $"Data Source={dbPath};";

builder.Services.AddDbContext<WorkScheduleContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();