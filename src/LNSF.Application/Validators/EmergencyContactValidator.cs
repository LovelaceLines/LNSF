using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
{
    public EmergencyContactValidator()
    {
        RuleFor(contact => contact.Name)
            .MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome", 3))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome", 64));
        
        RuleFor(contact => contact.Phone)
            .SetValidator(new PhoneValidator());
    }
}
