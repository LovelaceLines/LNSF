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
        
        RuleFor(x => x.OrderBy)
            .SetValidator(new OrderByValidator());
            
        RuleFor(x => x.Bathroom)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField);
        
        RuleFor(x => x.Beds)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField);
        
        RuleFor(x => x.Vacant)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField);

        RuleFor(x => x.Storey)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField);
        
        RuleFor(x => x.Available)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField);
    }
}
