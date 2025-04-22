using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Profession { get; set; }

    public string? Department { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
