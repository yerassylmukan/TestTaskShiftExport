namespace WebApplication1.Dtos;

public class ShiftDto
{
    public int EmployeeId { get; set; }
    public DateOnly ShiftDate { get; set; }
    public string ShiftRange { get; set; } = string.Empty;
    public double Hours { get; set; }
    public string DayNote { get; set; } = string.Empty;
}