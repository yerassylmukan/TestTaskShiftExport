namespace ConsoleApp1.Models;

// Моделька для хранения информации о сменах сотрудников
public class EmployeeShiftModel
{
    public string Department { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShiftRange { get; set; }
}