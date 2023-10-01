using FluentValidation;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class TourFiltersValidator : AbstractValidator<TourFilters>
{
    public TourFiltersValidator()
    {
        RuleFor(x => x.Page)
            .SetValidator(new PaginationValidator());
    }
}
