using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class WorkScheduleContext : DbContext
{
    public WorkScheduleContext()
    {
    }

    public WorkScheduleContext(DbContextOptions<WorkScheduleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employees");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Department).HasColumnName("department");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Profession).HasColumnName("profession");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("shifts");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.DayNote).HasColumnName("day_note");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Hours).HasColumnName("hours");
            entity.Property(e => e.ShiftDate).HasColumnName("shift_date");
            entity.Property(e => e.ShiftRange).HasColumnName("shift_range");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Shifts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;", cancellationToken);
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}