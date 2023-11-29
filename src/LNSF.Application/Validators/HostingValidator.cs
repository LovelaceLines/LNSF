using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class HostingValidator : AbstractValidator<Hosting>
{
    public HostingValidator()
    {
        RuleFor(hosting => hosting.CheckIn)
            .LessThan(hosting => hosting.CheckOut)
            .When(hosting => hosting.CheckOut.HasValue)
            .WithMessage("Data de saída deve ser maior que a data de entrada");
    }
}
