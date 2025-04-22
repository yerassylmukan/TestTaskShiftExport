using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Dtos;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ShiftController : ControllerBase
{
    private readonly IValidator<ShiftDto> _shiftDtoValidator;
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService, IValidator<ShiftDto> shiftDtoValidator)
    {
        _shiftService = shiftService;
        _shiftDtoValidator = shiftDtoValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetShifts()
    {
        var result = await _shiftService.GetShifts();
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShift(int id)
    {
        var result = await _shiftService.GetShift(id);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> AddShift([FromBody] ShiftDto shiftDto)
    {
        var validationResult = await _shiftDtoValidator.ValidateAsync(shiftDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var result = await _shiftService.AddShift(shiftDto);
        if (result.IsSuccess) return Ok("Смена успешно добавлена.");
        return BadRequest(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShift(int id, [FromBody] ShiftUpdateDto shiftDto)
    {
        var result = await _shiftService.UpdateShift(id, shiftDto);
        if (result.IsSuccess) return Ok("Смена успешно обновлена.");
        return BadRequest(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var result = await _shiftService.DeleteShift(id);
        if (result.IsSuccess) return Ok("Смена успешно удалена.");
        return BadRequest(result.ErrorMessage);
    }
}