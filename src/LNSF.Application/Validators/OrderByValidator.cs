using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class OrderByValidator : AbstractValidator<OrderBy>
{
    public OrderByValidator()
    {
        RuleFor(x => x)
            .NotNull().WithMessage(GlobalValidator.RequiredField("Ordenação"))
            .IsInEnum().WithMessage(GlobalValidator.InvalidField("Ordenação"));
    }
}
