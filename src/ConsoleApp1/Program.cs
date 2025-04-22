using System.Text;
using ConsoleApp1.Repositories;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string dbPath;
#if DEBUG
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string solutionDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\.."));
        dbPath = Path.Combine(solutionDir, "work_schedule.db");
#else
        dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "work_schedule.db");
#endif
        try
        {
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database file not found at: " + dbPath);
                return;
            }

            IShiftRepository shiftRepository = new ShiftRepository(dbPath);
            var shifts = shiftRepository.GetWorkDayShifts();

            if (shifts == null)
            {
                Console.WriteLine("No shift data retrieved.");
                return;
            }

            Console.WriteLine("| Отдел        | Имя        | Фамилия    | Время работы     |");
            Console.WriteLine("|--------------|------------|------------|------------------|");

            foreach (var shift in shifts)
            {
                Console.WriteLine($"| {shift.Department,-12} | {shift.FirstName,-10} | {shift.LastName,-10} | {shift.ShiftRange,-16} |");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Application error: " + ex.Message);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}