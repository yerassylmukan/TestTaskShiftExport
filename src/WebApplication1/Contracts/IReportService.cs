using WebApplication1.Common;
using WebApplication1.Dtos;

namespace WebApplication1.Contracts;

public interface IReportService
{
    Task<Result<IEnumerable<EmployeeShiftReportDto>>> GetEmployeeShiftReport();
}