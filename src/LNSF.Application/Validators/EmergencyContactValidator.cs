using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
{
    public EmergencyContactValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .MaximumLength(64)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .SetValidator(new PhoneValidator());
    }
}
