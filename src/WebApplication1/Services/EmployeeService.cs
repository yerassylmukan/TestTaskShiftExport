using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Contracts;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class EmployeeService : IEmployeeService
{
    private readonly WorkScheduleContext _context;

    public EmployeeService(WorkScheduleContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        try
        {
            var employees = await _context.Employees.ToListAsync();
            if (employees == null || !employees.Any())
                return Result<IEnumerable<EmployeeDto>>.FailureResult("Сотрудники не найдены.");

            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Profession = e.Profession,
                Department = e.Department,
                BirthDate = e.BirthDate,
                Address = e.Address
            }).ToList();

            return Result<IEnumerable<EmployeeDto>>.SuccessResult(employeeDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<EmployeeDto>>.FailureResult("Ошибка при получении сотрудников: " + ex.Message);
        }
    }

    public async Task<Result<EmployeeDto>> GetEmployee(int id)
    {
        try
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return Result<EmployeeDto>.FailureResult("Сотрудник не найден.");

            var employeeDto = new EmployeeDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Profession = employee.Profession,
                Department = employee.Department,
                BirthDate = employee.BirthDate,
                Address = employee.Address
            };

            return Result<EmployeeDto>.SuccessResult(employeeDto);
        }
        catch (Exception ex)
        {
            return Result<EmployeeDto>.FailureResult("Ошибка при получении сотрудника: " + ex.Message);
        }
    }

    public async Task<Result> AddEmployee(EmployeeDto employeeDto)
    {
        try
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Profession = employeeDto.Profession,
                Department = employeeDto.Department,
                BirthDate = employeeDto.BirthDate,
                Address = employeeDto.Address
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при добавлении сотрудника: " + ex.Message);
        }
    }

    public async Task<Result> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
    {
        try
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return Result.FailureResult("Сотрудник не найден.");

            if (!string.IsNullOrEmpty(employeeDto.FirstName))
                employee.FirstName = employeeDto.FirstName;

            if (!string.IsNullOrEmpty(employeeDto.LastName))
                employee.LastName = employeeDto.LastName;

            if (!string.IsNullOrEmpty(employeeDto.Profession))
                employee.Profession = employeeDto.Profession;

            if (!string.IsNullOrEmpty(employeeDto.Department))
                employee.Department = employeeDto.Department;

            if (employeeDto.BirthDate.HasValue)
                employee.BirthDate = employeeDto.BirthDate;

            if (!string.IsNullOrEmpty(employeeDto.Address))
                employee.Address = employeeDto.Address;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при обновлении сотрудника: " + ex.Message);
        }
    }

    public async Task<Result> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return Result.FailureResult("Сотрудник не найден.");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при удалении сотрудника: " + ex.Message);
        }
    }
}