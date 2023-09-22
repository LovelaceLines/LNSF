﻿using FluentValidation;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.Number)
            .MinimumLength(1).WithMessage(GlobalValidator.MinLength("Número", 1))
            .MaximumLength(8).WithMessage(GlobalValidator.MaxLength("Número", 8));
                
        RuleFor(room => room.Beds)
            .GreaterThan(0).WithMessage(GlobalValidator.MinLength("Camas", 1));
        
        RuleFor(room => room.Occupation)
            .LessThanOrEqualTo(room => room.Beds).WithMessage("Mais pessoas do que cama.")
            .GreaterThanOrEqualTo(0).WithMessage(GlobalValidator.MinLength("Ocupação", 0));
        
        RuleFor(room => room.Storey)
            .GreaterThan(0).WithMessage(GlobalValidator.MinLength("Andar", 1));
    }
}
