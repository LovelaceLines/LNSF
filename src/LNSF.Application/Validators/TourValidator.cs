using System.Drawing;
using FluentValidation;
using LNSF.Application.Services.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Application.Validators;

public class TourOutputValidator : AbstractValidator<Tour>
{
    public TourOutputValidator()
    {
        RuleFor(tour => tour.Output)
            .NotEmpty()
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");

        RuleFor(tour => tour.Note)
            .MaximumLength(256)
            .WithMessage("Máximo 256 caracteres.");
    }
}

public class TourInputValidator : AbstractValidator<Tour>
{
    GlobalValidator _globalValidator = new();

    public TourInputValidator(GlobalValidator globalValidator) => 
        _globalValidator = globalValidator;

    public TourInputValidator()
    {
        RuleFor(tour => tour.Id)
            .NotEmpty();
        
        RuleFor(tour => tour.Output)
            .NotNull()
            .NotEmpty()
            .Must(input => _globalValidator.IsDateFormatted(input))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");
        
        RuleFor(tour => tour.Note);
        
        RuleFor(tour => tour.Input)
            .NotEmpty()
            .Must(input => new GlobalValidator().IsDateFormatted(input))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");
        
        // TODO
        RuleFor(tour => tour.Input)
            .GreaterThan(tour => tour.Output);
    }
}
