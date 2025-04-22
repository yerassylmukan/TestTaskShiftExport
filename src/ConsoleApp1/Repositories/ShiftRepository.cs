using System.Data.SQLite;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly string _connectionString;

    public ShiftRepository(string dbPath)
    {
        if (string.IsNullOrEmpty(dbPath))
            throw new ArgumentException("Database path cannot be null or empty.", nameof(dbPath));

        _connectionString = $"Data Source={dbPath};Version=3;";
    }

    public IEnumerable<EmployeeShift> GetWorkDayShifts()
    {
        var shifts = new List<EmployeeShift>();

        try
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var query = @"
                    SELECT 
                        e.department,
                        e.first_name,
                        e.last_name,
                        s.shift_range
                    FROM shifts s
                    JOIN employees e ON s.employee_id = e.id
                    WHERE s.day_note = 'рабочий день'
                    ORDER BY e.department, e.last_name;
                ";

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
                shifts.Add(new EmployeeShift
                {
                    Department = reader.GetString(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    ShiftRange = reader.GetString(3)
                });
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine("SQLite error: " + ex.Message);
            LogError(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            LogError(ex);
        }

        return shifts;
    }

    private void LogError(Exception ex)
    {
        Console.WriteLine($"Error logged: {ex}");
    }
}