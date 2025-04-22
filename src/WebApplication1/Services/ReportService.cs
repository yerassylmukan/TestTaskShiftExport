using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Contracts;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ReportService : IReportService
{
    private readonly WorkScheduleContext _context;

    public ReportService(WorkScheduleContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<EmployeeShiftReportDto>>> GetEmployeeShiftReport()
    {
        try
        {
            var reportData = await _context.Shifts
                .Where(s => s.DayNote == "рабочий день")
                .Include(s => s.Employee)
                .OrderBy(s => s.Employee.Department)
                .ThenBy(s => s.Employee.LastName)
                .Select(s => new EmployeeShiftReportDto
                {
                    Department = s.Employee.Department,
                    FirstName = s.Employee.FirstName,
                    LastName = s.Employee.LastName,
                    ShiftRange = s.ShiftRange
                })
                .ToListAsync();

            if (reportData == null || !reportData.Any())
                return Result<IEnumerable<EmployeeShiftReportDto>>.FailureResult("Смены не найдены.");

            return Result<IEnumerable<EmployeeShiftReportDto>>.SuccessResult(reportData);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<EmployeeShiftReportDto>>.FailureResult("Ошибка при получении данных отчета: " +
                                                                             ex.Message);
        }
    }
}