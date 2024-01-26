using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class FamilyGroupProfileValidator : AbstractValidator<FamilyGroupProfile>
{
    public FamilyGroupProfileValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty().WithMessage(GlobalValidator.RequiredField("Nome"))
            .MaximumLength(128).WithMessage(GlobalValidator.MaxLength("Nome", 128));

        RuleFor(f => f.Kinship)
            .NotEmpty().WithMessage(GlobalValidator.RequiredField("Parentesco"))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Parentesco", 64));

        RuleFor(f => f.Age)
            .GreaterThanOrEqualTo(0).WithMessage(GlobalValidator.InvalidAge())
            .LessThanOrEqualTo(120).WithMessage(GlobalValidator.InvalidAge());

        RuleFor(f => f.Profession)
            .NotEmpty().WithMessage(GlobalValidator.RequiredField("Profissão"))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Profissão", 64));

        RuleFor(f => f.Income)
            .GreaterThanOrEqualTo(0).WithMessage(GlobalValidator.MinLength("Renda", 0));
    }
}
