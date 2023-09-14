using FluentValidation;

namespace LNSF.Application.Validators;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Matches(@"^\(\d{2}\) \d \d{4}-\d{4}$")
            .WithMessage(GlobalValidator.InvalidPhoneFormat);
    }
}