using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeeShiftReport(DateOnly reportDate)
    {
        var result = await _reportService.GetEmployeeShiftReport(reportDate);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }
}