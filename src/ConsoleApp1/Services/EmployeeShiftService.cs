using ClosedXML.Excel;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using ConsoleApp1.Services;

// Сервис класс (бизнес-логика) для обновления Excel файла
public class EmployeeShiftService : IEmployeeShiftService
{
    private readonly string _excelFilePath;
    private readonly IShiftRepository _shiftRepository;

    public EmployeeShiftService(string excelFilePath, IShiftRepository shiftRepository)
    {
        if (string.IsNullOrEmpty(excelFilePath))
            throw new ArgumentException("Excel file path cannot be null or empty.", nameof(excelFilePath));

        if (shiftRepository == null)
            throw new ArgumentNullException(nameof(shiftRepository), "Shift repository cannot be null.");

        _excelFilePath = excelFilePath;
        _shiftRepository = shiftRepository;
    }

    // Метод для обновления Excel файла с данными о сменах
    public void UpdateExcelFile()
    {
        try
        {
            // Получаем смены сотрудников для рабочих дней
            var shifts = _shiftRepository.GetWorkDayShifts();

            if (shifts == null || !shifts.Any())
            {
                Console.WriteLine("No workday shifts found.");
                return;
            }

            // Обновление Excel файла
            UpdateExcelWithShifts(shifts);
            Console.WriteLine("Excel file updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating the Excel file: {ex.Message}");
            LogError(ex);
        }
    }

    // Метод для обновления Excel файла с полученными сменами
    private void UpdateExcelWithShifts(IEnumerable<EmployeeShiftModel> shifts)
    {
        try
        {
            using var workbook = new XLWorkbook(_excelFilePath);
            var worksheet = workbook.Worksheet(1);

            var row = 2;

            foreach (var shift in shifts)
            {
                worksheet.Cell(row, 1).Value = shift.Department;
                worksheet.Cell(row, 2).Value = shift.FirstName;
                worksheet.Cell(row, 3).Value = shift.LastName;
                worksheet.Cell(row, 4).Value = shift.ShiftRange;

                row++;
            }

            workbook.Save();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Excel file error: {ex.Message}");
            LogError(ex);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error while accessing the Excel file: {ex.Message}");
            LogError(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while updating the Excel file: {ex.Message}");
            LogError(ex);
        }
    }

    // Метод для логирования ошибок
    private void LogError(Exception ex)
    {
        Console.WriteLine($"Error logged: {ex}");
    }
}