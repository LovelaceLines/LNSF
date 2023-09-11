using FluentValidation;
using LNSF.Application.Services.Validators;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TourPostValidator : AbstractValidator<Tour>
{
    public TourPostValidator()
    {
        RuleFor(tour => tour.Output)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage(GlobalValidator.InvalidDateTimeFormat);

        RuleFor(tour => tour.Note)
            .MaximumLength(256)
            .WithMessage(GlobalValidator.MaxLength);
    }
}

public class TourPutValidator : AbstractValidator<Tour>
{
    public TourPutValidator()
    {
        RuleFor(tour => tour.Output)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage(GlobalValidator.InvalidDateTimeFormat);
        
        RuleFor(tour => tour.Input)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage(GlobalValidator.InvalidDateTimeFormat)
            .GreaterThan(tour => tour.Output)
            .WithMessage(GlobalValidator.InvalidOutputDate);

        RuleFor(tour => tour.Note)
            .MaximumLength(256)
            .WithMessage(GlobalValidator.MaxLength);
    }
}
