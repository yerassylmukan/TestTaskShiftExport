using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Dtos;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeeController : ControllerBase
{
    private readonly IValidator<EmployeeDto> _employeeDtoValidator;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService, IValidator<EmployeeDto> employeeDtoValidator)
    {
        _employeeService = employeeService;
        _employeeDtoValidator = employeeDtoValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var result = await _employeeService.GetEmployees();
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var result = await _employeeService.GetEmployee(id);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
    {
        var validationResult = await _employeeDtoValidator.ValidateAsync(employeeDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var result = await _employeeService.AddEmployee(employeeDto);
        if (result.IsSuccess) return Ok("Сотрудник успешно добавлен.");
        return BadRequest(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeDto)
    {
        var result = await _employeeService.UpdateEmployee(id, employeeDto);
        if (result.IsSuccess) return Ok("Сотрудник успешно обновлен.");
        return BadRequest(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _employeeService.DeleteEmployee(id);
        if (result.IsSuccess) return Ok("Сотрудник успешно удален.");
        return BadRequest(result.ErrorMessage);
    }
}