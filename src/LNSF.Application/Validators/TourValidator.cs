using FluentValidation;
using LNSF.Domain.DTOs;
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

public class TourFiltersValidator : AbstractValidator<TourFilters>
{
    public TourFiltersValidator()
    {
        RuleFor(x => x.Page)
            .SetValidator(new PaginationValidator());
    }
}
