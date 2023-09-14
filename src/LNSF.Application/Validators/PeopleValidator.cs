using System.Globalization;
using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class PeopleValidator : AbstractValidator<People>
{
    public PeopleValidator()
    {
        RuleFor(people => people.Name)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .MaximumLength(64)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(people => people.BirthDate.Year)
            .LessThan(DateTime.Now.Year - 15)
            .WithMessage(GlobalValidator.InvalidAge)
            .GreaterThanOrEqualTo(128)
            .WithMessage(GlobalValidator.InvalidAge);
        
        RuleFor(people => people.BirthDate)
            .SetValidator(new DateValidator());
        
        RuleFor(people => people.RG)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Matches(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$")
            .WithMessage(GlobalValidator.InvalidRGFormat);

        RuleFor(people => people.CPF)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
            .WithMessage(GlobalValidator.InvalidCPFFormat);
        
        RuleFor(people => people.Street)
            .MaximumLength(64)
            .WithMessage(GlobalValidator.MaxLength);

        RuleFor(people => people.HouseNumber)
            .MaximumLength(8)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(people => people.Neighborhood)
            .MaximumLength(32)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(people => people.City)
            .MaximumLength(32)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(people => people.State)
            .MaximumLength(16)
            .WithMessage(GlobalValidator.MaxLength);
        
        RuleFor(people => people.Phone)
            .MaximumLength(21)
            .WithMessage(GlobalValidator.MaxLength)
            .SetValidator(new PhoneValidator());
        
        RuleFor(people => people.Note)
            .MaximumLength(256)
            .WithMessage(GlobalValidator.MaxLength);
    }
}
