using FluentValidation;
using LNSF.Domain;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.Number)
            .NotEmpty();
        
        RuleFor(room => room.Bathroom);
        
        RuleFor(room => room.Beds)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(room => room.Occupation)
            .LessThanOrEqualTo(room => room.Beds)
            .WithMessage("Mais pessoas do que cama.")
            .GreaterThanOrEqualTo(0);
        
        RuleFor(room => room.Storey)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(room => room.Available);
    }
}
