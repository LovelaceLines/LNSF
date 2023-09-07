using FluentValidation;
using LNSF.Application.Services.Validators;
using LNSF.Domain.Views;

namespace LNSF.Application.Validators;

public class TourOutputValidator : AbstractValidator<ITourOutput>
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

public class TourInputValidator : AbstractValidator<ITourInput>
{
    public TourInputValidator()
    {
        RuleFor(tour => tour.Input)
            .NotEmpty()
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");
    }
}
