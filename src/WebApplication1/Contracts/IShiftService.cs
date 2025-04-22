using WebApplication1.Common;
using WebApplication1.Dtos;

namespace WebApplication1.Contracts;

public interface IShiftService
{
    Task<Result<IEnumerable<ShiftDto>>> GetShifts();
    Task<Result<ShiftDto>> GetShift(int id);
    Task<Result> AddShift(ShiftDto shift);
    Task<Result> UpdateShift(int id, ShiftUpdateDto shift);
    Task<Result> DeleteShift(int id);
}