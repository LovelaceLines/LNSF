using FluentValidation;

namespace LNSF.Application.Validators;

public class DateTimeValidator : AbstractValidator<DateTime>
{
    public DateTimeValidator()
    {
        RuleFor(date => date.ToString("dd/MM/yyyy HH:mm:ss tt"))
            .Matches(@"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2} (AM|PM)$")
            .WithMessage(GlobalValidator.InvalidDateTimeFormat());
    }
}
