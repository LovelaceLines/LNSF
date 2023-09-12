using FluentValidation;
using LNSF.Application.Services.Validators;
using LNSF.Domain.DTOs;

namespace LNSF.Application;

public class OrderByValidator : AbstractValidator<OrderBy>
{
    public OrderByValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage(GlobalValidator.RequiredField)
            .IsInEnum()
            .WithMessage(GlobalValidator.InvalidField);
    }
}
