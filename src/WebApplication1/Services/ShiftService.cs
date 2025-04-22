using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Contracts;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ShiftService : IShiftService
{
    private readonly WorkScheduleContext _context;

    public ShiftService(WorkScheduleContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<ShiftDto>>> GetShifts()
    {
        try
        {
            var shifts = await _context.Shifts
                .Include(s => s.Employee)
                .ToListAsync();

            if (shifts == null || !shifts.Any())
                return Result<IEnumerable<ShiftDto>>.FailureResult("Смены не найдены.");

            var shiftDtos = shifts.Select(s => new ShiftDto
            {
                EmployeeId = s.EmployeeId ?? 0,
                ShiftDate = s.ShiftDate ?? DateOnly.MinValue,
                ShiftRange = s.ShiftRange ?? string.Empty,
                Hours = s.Hours ?? 0,
                DayNote = s.DayNote ?? string.Empty
            }).ToList();

            return Result<IEnumerable<ShiftDto>>.SuccessResult(shiftDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ShiftDto>>.FailureResult("Ошибка при получении смен: " + ex.Message);
        }
    }

    public async Task<Result<ShiftDto>> GetShift(int id)
    {
        try
        {
            var shift = await _context.Shifts
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shift == null) return Result<ShiftDto>.FailureResult("Смена не найдена.");

            var shiftDto = new ShiftDto
            {
                EmployeeId = shift.EmployeeId ?? 0,
                ShiftDate = shift.ShiftDate ?? DateOnly.MinValue,
                ShiftRange = shift.ShiftRange ?? string.Empty,
                Hours = shift.Hours ?? 0,
                DayNote = shift.DayNote ?? string.Empty
            };

            return Result<ShiftDto>.SuccessResult(shiftDto);
        }
        catch (Exception ex)
        {
            return Result<ShiftDto>.FailureResult("Ошибка при получении смены: " + ex.Message);
        }
    }

    public async Task<Result> AddShift(ShiftDto shiftDto)
    {
        try
        {
            var shift = new Shift
            {
                EmployeeId = shiftDto.EmployeeId,
                ShiftDate = shiftDto.ShiftDate,
                ShiftRange = shiftDto.ShiftRange,
                Hours = shiftDto.Hours,
                DayNote = shiftDto.DayNote
            };

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при добавлении смены: " + ex.Message);
        }
    }

    public async Task<Result> UpdateShift(int id, ShiftUpdateDto shiftDto)
    {
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return Result.FailureResult("Смена не найдена.");

            if (shiftDto.EmployeeId > 0)
                shift.EmployeeId = shiftDto.EmployeeId;

            if (shiftDto.ShiftDate != default)
                shift.ShiftDate = shiftDto.ShiftDate;

            if (!string.IsNullOrEmpty(shiftDto.ShiftRange))
                shift.ShiftRange = shiftDto.ShiftRange;

            if (shiftDto.Hours > 0)
                shift.Hours = shiftDto.Hours;

            if (!string.IsNullOrEmpty(shiftDto.DayNote))
                shift.DayNote = shiftDto.DayNote;

            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при обновлении смены: " + ex.Message);
        }
    }

    public async Task<Result> DeleteShift(int id)
    {
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return Result.FailureResult("Смена не найдена.");

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            return Result.FailureResult("Ошибка при удалении смены: " + ex.Message);
        }
    }
}