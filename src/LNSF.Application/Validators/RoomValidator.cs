using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.Number)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .MaximumLength(8)
            .WithMessage(GlobalValidator.MaxLength);
        
        // RuleFor(room => room.Bathroom);
        
        RuleFor(room => room.Beds)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .GreaterThan(0)
            .WithMessage(GlobalValidator.MinLength);
        
        RuleFor(room => room.Occupation)
            .LessThanOrEqualTo(room => room.Beds)
            .WithMessage("Mais pessoas do que cama.")
            .GreaterThanOrEqualTo(0)
            .WithMessage(GlobalValidator.MinLength);
        
        RuleFor(room => room.Storey)
            .NotEmpty()
            .WithMessage(GlobalValidator.RequiredField)
            .GreaterThan(0)
            .WithMessage(GlobalValidator.MinLength);

        // RuleFor(room => room.Available);
    }
}
