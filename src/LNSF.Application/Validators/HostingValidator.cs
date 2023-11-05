using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class HostingValidator : AbstractValidator<Hosting>
{
    public HostingValidator()
    {
        RuleFor(x => x.CheckOut)
            .GreaterThan(x => x.CheckIn)
            .WithMessage("Data de saída deve ser maior que a data de entrada");
    }
}
