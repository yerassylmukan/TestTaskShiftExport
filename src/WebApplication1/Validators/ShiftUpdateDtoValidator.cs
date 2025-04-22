using FluentValidation;
using WebApplication1.Dtos;

namespace WebApplication1.Validators;

public class ShiftUpdateDtoValidator : AbstractValidator<ShiftUpdateDto>
{
    public ShiftUpdateDtoValidator()
    {
        RuleFor(s => s.EmployeeId)
            .GreaterThan(0).WithMessage("ID сотрудника должен быть больше нуля.")
            .When(s => s.EmployeeId != null);

        RuleFor(s => s.ShiftDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Дата смены не может быть в будущем.")
            .When(s => s.ShiftDate != null);

        RuleFor(s => s.ShiftRange)
            .Matches(@"^\d{2}:\d{2}-\d{2}:\d{2}$").WithMessage("Время смены должно быть в формате HH:mm-HH:mm.")
            .When(s => !string.IsNullOrEmpty(s.ShiftRange));

        RuleFor(s => s.Hours)
            .GreaterThan(0).WithMessage("Часы работы должны быть больше нуля.")
            .When(s => s.Hours != null);

        RuleFor(s => s.DayNote)
            .Must(note =>
                note == "рабочий день" || note == "выходной" || note == "гос праздник" || note == "больничный")
            .WithMessage("Тип дня должен быть одним из: 'рабочий день', 'выходной', 'гос праздник', 'больничный'.")
            .When(s => !string.IsNullOrEmpty(s.DayNote));
    }
}