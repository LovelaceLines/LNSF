using FluentValidation;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class PeopleFiltersValidator : AbstractValidator<PeopleFilters>
{
    public PeopleFiltersValidator()
    {
        RuleFor(x => x.Page)
            .SetValidator(new PaginationValidator());
    }
}
