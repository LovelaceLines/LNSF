using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Validators;

public class RoleValidator : AbstractValidator<IdentityRole>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome do perfil", 4))
            .MaximumLength(16).WithMessage(GlobalValidator.MaxLength("Nome do perfil", 16));
    }
}
