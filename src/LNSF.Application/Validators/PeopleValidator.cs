using System.Data;
using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class PeopleValidator : AbstractValidator<People>
{
    public PeopleValidator()
    {
        RuleFor(people => people.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64);
        
        RuleFor(people => people.BirthDate)
            .LessThan(DateTime.Now)
            .GreaterThan(DateTime.Now.AddYears(-120));
        
        RuleFor(people => people.RG)
            .NotEmpty()
            .Matches(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$")
            .WithMessage("RG inválido. Use o formato XX.XXX.XXX-X");

        RuleFor(people => people.CPF)
            .NotEmpty()
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
            .WithMessage("CPF inválido. Use o formato XXX.XXX.XXX-XX");
        
        RuleFor(people => people.Street)
            .MaximumLength(64);

        RuleFor(people => people.HouseNumber)
            .MaximumLength(8);
        
        RuleFor(people => people.Neighborhood)
            .MaximumLength(32);
        
        RuleFor(people => people.City)
            .MaximumLength(32);
        
        RuleFor(people => people.State)
            .MaximumLength(16);
        
        RuleFor(people => people.Note)
            .MaximumLength(256);
    }
}
