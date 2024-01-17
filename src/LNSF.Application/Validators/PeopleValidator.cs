using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class PeopleValidator : AbstractValidator<People>
{
    public PeopleValidator()
    {
        RuleFor(people => people.Name)
            .MinimumLength(2).WithMessage(GlobalValidator.MinLength("Nome", 2))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome", 64));
        
        RuleFor(people => people.BirthDate.Year)
            .LessThanOrEqualTo(DateTime.Now.Year - 15).WithMessage(GlobalValidator.InvalidAge())
            .GreaterThanOrEqualTo(DateTime.Now.Year - 128).WithMessage(GlobalValidator.InvalidAge());
        
        RuleFor(people => people.RG)
            .Matches(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$").WithMessage(GlobalValidator.InvalidRGFormat());
        
        RuleFor(people => people.IssuingBody)
            .MaximumLength(16).WithMessage(GlobalValidator.MaxLength("Órgão Emissor", 16))
            .Matches(@"^[A-Z]+(?:-[A-Z]+)?$").WithMessage(GlobalValidator.InvalidIssuingBodyFormat());

        RuleFor(people => people.CPF)
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage(GlobalValidator.InvalidCPFFormat());
        
        RuleFor(people => people.Street)
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Rua", 64));

        RuleFor(people => people.HouseNumber)
            .MaximumLength(8).WithMessage(GlobalValidator.MaxLength("Número", 8));
        
        RuleFor(people => people.Neighborhood)
            .MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Bairro", 32));
        
        RuleFor(people => people.City)
            .MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Cidade", 32));
        
        RuleFor(people => people.State)
            .MaximumLength(16).WithMessage(GlobalValidator.MaxLength("Estado", 16));
        
        RuleFor(people => people.Phone)
            .SetValidator(new PhoneValidator());
        
        RuleFor(people => people.Note)
            .MaximumLength(256).WithMessage(GlobalValidator.MaxLength("Observação", 256));
    }
}
