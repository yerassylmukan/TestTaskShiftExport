using FluentValidation;
using WebApplication1.Dtos;

namespace WebApplication1.Validators;

public class ShiftDtoValidator : AbstractValidator<ShiftDto>
{
    public ShiftDtoValidator()
    {
        RuleFor(s => s.EmployeeId)
            .GreaterThan(0).WithMessage("ID сотрудника должен быть больше нуля.");

        RuleFor(s => s.ShiftDate)
            .NotEmpty().WithMessage("Дата смены не может быть пустой.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Дата смены не может быть в будущем.");

        RuleFor(s => s.ShiftRange)
            .NotEmpty().WithMessage("Время смены не может быть пустым.")
            .Matches(@"^\d{2}:\d{2}-\d{2}:\d{2}$").WithMessage("Время смены должно быть в формате HH:mm-HH:mm.");

        RuleFor(s => s.Hours)
            .GreaterThan(0).WithMessage("Часы работы должны быть больше нуля.");

        RuleFor(s => s.DayNote)
            .NotEmpty().WithMessage("Тип дня не может быть пустым.")
            .Must(note =>
                note == "рабочий день" || note == "выходной" || note == "гос праздник" || note == "больничный")
            .WithMessage("Тип дня должен быть одним из: 'рабочий день', 'выходной', 'гос праздник', 'больничный'.");
    }
}