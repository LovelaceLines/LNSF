using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {   
        RuleFor(page => page.Page)
            .GreaterThan(0)
            .WithMessage(GlobalValidator.MinLength);
        
        RuleFor(page => page.PageSize)
            .GreaterThan(0)
            .WithMessage(GlobalValidator.MinLength);
    }
}
