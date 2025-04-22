using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories;

public interface IShiftRepository
{
    IEnumerable<EmployeeShift> GetWorkDayShifts();
}