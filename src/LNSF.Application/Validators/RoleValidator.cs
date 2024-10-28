using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class RoleValidator : AbstractValidator<Role>
{
	public RoleValidator()
	{
		RuleFor(x => x.Name)
			.MinimumLength(4).WithMessage(GlobalValidator.MinLength("Nome da permissão", 4))
			.MaximumLength(32).WithMessage(GlobalValidator.MaxLength("Nome da permissão", 32));
	}
}
