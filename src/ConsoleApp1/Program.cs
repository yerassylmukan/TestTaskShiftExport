using System.Text;
using ConsoleApp1.Repositories;
using ConsoleApp1.Services;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string dbPath;
        string excelPath;
#if DEBUG
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var solutionDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\.."));
        dbPath = Path.Combine(solutionDir, "work_schedule.db");
        excelPath = Path.Combine(solutionDir, "excel.xlsx");
#else
        dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "work_schedule.db");
        excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excel.xlsx");
#endif
        try
        {
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database file not found at: " + dbPath);
                return;
            }

            if (!File.Exists(excelPath))
            {
                Console.WriteLine("Excel file not found at: " + excelPath);
                return;
            }

            IShiftRepository shiftRepository = new ShiftRepository(dbPath);
            IEmployeeShiftService employeeShiftService = new EmployeeShiftService(excelPath, shiftRepository);
            employeeShiftService.UpdateExcelFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Application error: " + ex.Message);
        }
    }
}