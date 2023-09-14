using FluentValidation;

namespace LNSF.Application.Validators;

public class DateTimeValidator : AbstractValidator<DateTime>
{
    public DateTimeValidator()
    {
        RuleFor(date => date.ToString())
            .Matches(@"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}$")
            .WithMessage(GlobalValidator.InvalidDateFormat);
    }
}
