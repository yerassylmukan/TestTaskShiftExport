using FluentValidation;
using WebApplication1.Dtos;

namespace WebApplication1.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("Имя не может быть пустым.")
            .MaximumLength(50).WithMessage("Имя не может быть длиннее 50 символов.");

        RuleFor(e => e.LastName)
            .NotEmpty().WithMessage("Фамилия не может быть пустой.")
            .MaximumLength(50).WithMessage("Фамилия не может быть длиннее 50 символов.");

        RuleFor(e => e.Profession)
            .NotEmpty().WithMessage("Должность не может быть пустой.");

        RuleFor(e => e.Department)
            .NotEmpty().WithMessage("Отдел не может быть пустым.");

        RuleFor(e => e.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Дата рождения должна быть в прошлом.");

        RuleFor(e => e.Address)
            .MaximumLength(200).WithMessage("Адрес не может быть длиннее 200 символов.");
    }
}