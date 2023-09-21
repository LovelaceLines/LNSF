using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class RoomFiltersValidator : AbstractValidator<RoomFilters>
{
    public RoomFiltersValidator()
    {
        RuleFor(x => x.Page)
            .SetValidator(new PaginationValidator());
        
        RuleFor(x => x.Order)
            .SetValidator(new OrderByValidator());
    }
}
