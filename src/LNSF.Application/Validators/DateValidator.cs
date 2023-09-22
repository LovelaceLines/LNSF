using FluentValidation;

namespace LNSF.Application.Validators;

public class DateValidator : AbstractValidator<DateTime>
{
    public DateValidator()
    {
        RuleFor(date => date.ToString("dd-MM-yyyy"))
            .Matches(@"^\d{2}-\d{2}-\d{4}$").WithMessage(GlobalValidator.InvalidDateFormat());
    }
}
