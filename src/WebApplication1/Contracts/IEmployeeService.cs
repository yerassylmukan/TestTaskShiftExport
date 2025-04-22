using WebApplication1.Common;
using WebApplication1.Dtos;

namespace WebApplication1.Contracts;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeDto>>> GetEmployees();
    Task<Result<EmployeeDto>> GetEmployee(int id);
    Task<Result> AddEmployee(EmployeeDto employee);
    Task<Result> UpdateEmployee(int id, EmployeeUpdateDto employee);
    Task<Result> DeleteEmployee(int id);
}