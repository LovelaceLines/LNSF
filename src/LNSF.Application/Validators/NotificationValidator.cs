using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class NotificationValidator : AbstractValidator<Notification>
{
	public NotificationValidator()
	{
		RuleFor(x => x.ExpiredAt)
			.Must((x, y) => y > x.ValidFrom).WithMessage("Data de expiração inválida");
	}
}
