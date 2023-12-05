using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Validators;

public class UserValidator : AbstractValidator<IdentityUser>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome do usuário", 4))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome do usuário", 64));
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(GlobalValidator.InvalidEmailFormat());
        
        RuleFor(x => x.PhoneNumber!)
            .SetValidator(new PhoneValidator())
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }
}
