using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class HospitalValidator : AbstractValidator<Hospital>
{
    public HospitalValidator()
    {
        RuleFor(h => h.Name)
            .MinimumLength(6).WithMessage(GlobalValidator.MinLength("Nome", 6))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome", 64));
    }
}
