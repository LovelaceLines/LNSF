using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TreatmentValidator : AbstractValidator<Treatment>
{
    public TreatmentValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome do tratamento", 3)) 
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome do tratamento", 64));
    }
}