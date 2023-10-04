using FluentValidation;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class TourValidator : AbstractValidator<Tour>
{
    public TourValidator()
    {
        RuleFor(tour => tour.Note)
            .MaximumLength(256).WithMessage(GlobalValidator.MaxLength("Observação", 256));
    }
}

public class TourFilterValidator : AbstractValidator<TourFilter>
{
    public TourFilterValidator()
    {
        RuleFor(x => x.Page)
            .SetValidator(new PaginationValidator());
    }
}
