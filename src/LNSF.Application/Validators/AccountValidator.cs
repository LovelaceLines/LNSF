using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class AccountCreateValidator : AbstractValidator<Account>
{
    public AccountCreateValidator()
    {
        RuleFor(account => account.UserName)
            .MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome", 4))
            .MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Nome", 32));
        
        RuleFor(account => account.Password)
            .SetValidator(new PasswordValidator());
        
        RuleFor(account => account.Role)
            .IsInEnum().WithMessage("Cargo inválido!");
    }
}

public class AccountUpdateValidator : AbstractValidator<Account>
{
    public AccountUpdateValidator()
    {
        RuleFor(account => account.UserName)
            .MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome", 4))
            .MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Nome", 32));
        
        RuleFor(account => account.Role)
            .IsInEnum().WithMessage("Cargo inválido!");
    }
}
