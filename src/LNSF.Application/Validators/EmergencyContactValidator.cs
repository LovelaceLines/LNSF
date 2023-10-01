using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
{
    public EmergencyContactValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome", 3))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome", 64));
        
        RuleFor(x => x.Phone)
            .SetValidator(new PhoneValidator());
    }
}
