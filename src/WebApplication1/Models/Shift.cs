using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Shift
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? ShiftDate { get; set; }

    public string? ShiftRange { get; set; }

    public double? Hours { get; set; }

    public string? DayNote { get; set; }

    public virtual Employee? Employee { get; set; }
}
