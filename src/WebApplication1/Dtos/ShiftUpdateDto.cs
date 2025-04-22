namespace WebApplication1.Dtos;

public class ShiftUpdateDto
{
    public int? EmployeeId { get; set; }
    public DateOnly? ShiftDate { get; set; }
    public string? ShiftRange { get; set; }
    public double? Hours { get; set; }
    public string? DayNote { get; set; }
}