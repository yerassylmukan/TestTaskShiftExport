using System.Text;
using ConsoleApp1.Repositories;
using ConsoleApp1.Services;

public class Program
{
    public static void Main()
    {
        // Установка кодировки для корректного отображения символов
        Console.OutputEncoding = Encoding.UTF8;
        string dbPath;
        string excelPath;
        
        // Определение путей к базе данных и Excel файлу в зависимости от конфигурации
#if DEBUG // DEBUG означает, что приложение работает в режиме отладки
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        
        // Получение пути к корню решения, путем восхождения на несколько уровней вверх
        var solutionDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\.."));
        
        dbPath = Path.Combine(solutionDir, "work_schedule.db");
        excelPath = Path.Combine(solutionDir, "excel.xlsx");
#else // Выполняется эта часть, если приложение в релизной сборке
        // В релизной сборке путь к базе данных и Excel файлу будет относительным к текущей директории приложения
        dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "work_schedule.db");
        excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excel.xlsx");
#endif
        try
        {
            // Проверка существования базы данных
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database file not found at: " + dbPath);
                return;
            }

            // Проверка существования Excel файла
            if (!File.Exists(excelPath))
            {
                Console.WriteLine("Excel file not found at: " + excelPath);
                return;
            }

            // Создание репозитория смен и сервиса для обновления Excel файла
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