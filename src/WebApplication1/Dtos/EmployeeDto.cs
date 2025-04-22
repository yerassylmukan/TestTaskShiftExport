namespace WebApplication1.Dtos;

public class EmployeeDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Profession { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public string? Address { get; set; } = string.Empty;
}