using FluentValidation;
using LNSF.Application.Services.Validators;
using LNSF.Domain.Views;

namespace LNSF.Application.Validators;

public class ToorOutputValidator : AbstractValidator<IToorOutput>
{
    public ToorOutputValidator()
    {
        RuleFor(toor => toor.Output)
            .NotEmpty()
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");

        RuleFor(toor => toor.Note)
            .MaximumLength(256)
            .WithMessage("Máximo 256 caracteres.");
    }
}

public class ToorInputValidator : AbstractValidator<IToorInput>
{
    public ToorInputValidator()
    {
        RuleFor(toor => toor.Input)
            .NotEmpty()
            .Must(output => new GlobalValidator().IsDateFormatted(output))
            .WithMessage("Formato de data 'dd/MM/yyyy HH:mm:ss'");
    }
}
