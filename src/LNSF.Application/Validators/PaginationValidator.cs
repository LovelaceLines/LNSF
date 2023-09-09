using FluentValidation;
using LNSF.Domain.Views;

namespace LNSF.Application;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {   
        RuleFor(page => page.Page)
            .GreaterThan(0)
            .WithMessage("Pagina deve ser maior que 0");
        
        RuleFor(page => page.PageSize)
            .GreaterThan(0)
            .WithMessage("Tamanho deve ser maior que 0");
    }
}
