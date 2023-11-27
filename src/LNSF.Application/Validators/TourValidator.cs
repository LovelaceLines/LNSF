using FluentValidation;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TourValidator : AbstractValidator<Tour>
{
    public TourValidator()
    {
        RuleFor(tour => tour.Output)
            .LessThan(tour => tour.Input)
            .When(tour => tour.Input != null)
            .WithMessage("Data de saída deve ser menor que a data de retorno!");

        RuleFor(tour => tour.Note)
            .MaximumLength(256).WithMessage(GlobalValidator.MaxLength("Observação", 256));
    }
}
