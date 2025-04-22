namespace WebApplication1.Dtos;

public class EmployeeUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Profession { get; set; }
    public string? Department { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Address { get; set; }
}