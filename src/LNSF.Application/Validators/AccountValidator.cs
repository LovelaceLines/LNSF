using FluentValidation;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(account => account.UserName)
            .MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome", 2))
            .MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Nome", 32));
        
        RuleFor(account => account.Password)
            .SetValidator(new PasswordValidator());
        
        RuleFor(account => account.Role)
            .IsInEnum().WithMessage("Cargo inválido!");
    }
}

public class AccountFilterValidator : AbstractValidator<AccountFilter>
{
    public AccountFilterValidator()
    {
        RuleFor(filter => filter.Page)
            .SetValidator(new PaginationValidator());
        
        RuleFor(filter => filter.OrderBy)
            .SetValidator(new OrderByValidator());
    }
}
