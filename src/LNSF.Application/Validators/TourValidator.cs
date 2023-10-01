using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TourPostValidator : AbstractValidator<Tour>
{
    public TourPostValidator()
    {
        RuleFor(tour => tour.Note)
            .MaximumLength(256).WithMessage(GlobalValidator.MaxLength("Observação", 256));
    }
}

public class TourPutValidator : AbstractValidator<Tour>
{
    public TourPutValidator()
    {   
        RuleFor(tour => tour)
            .SetValidator(new TourPostValidator());
    }
}
