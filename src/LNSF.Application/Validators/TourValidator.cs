using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TourPostValidator : AbstractValidator<Tour>
{
    public TourPostValidator()
    {
        RuleFor(tour => tour.Output)
            .SetValidator(new DateTimeValidator());

        RuleFor(tour => tour.Note)
            .MaximumLength(256).WithMessage(GlobalValidator.MaxLength("Observação", 256));
    }
}

public class TourPutValidator : AbstractValidator<Tour>
{
    public TourPutValidator()
    {   
        RuleFor(tour => tour.Input)
            .GreaterThan(tour => tour.Output).WithMessage("Data de retorno deve ser maior que a data de saída.");
        
        RuleFor(tour => tour.Input ?? DateTime.Now)
            .SetValidator(new DateTimeValidator());
        
        RuleFor(tour => tour)
            .SetValidator(new TourPostValidator());
    }
}
