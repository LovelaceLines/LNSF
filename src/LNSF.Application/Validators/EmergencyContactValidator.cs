using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
{
	public EmergencyContactValidator()
	{
		RuleFor(contact => contact.Name)
			.MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome", 3))
			.MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome", 64));

		RuleFor(contact => contact.Phone)
			.MinimumLength(8).WithMessage(GlobalValidator.MinLength("Telefone", 8))
			.MaximumLength(16).WithMessage(GlobalValidator.MaxLength("Telefone", 16));
	}
}
