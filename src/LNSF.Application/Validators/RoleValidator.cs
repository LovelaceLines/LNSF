using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class RoleValidator : AbstractValidator<Role>
{
	public RoleValidator()
	{
		RuleFor(x => x.Name)
			.MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome do perfil", 4))
			.MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Nome do perfil", 32));
	}
}
