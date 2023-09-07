using FluentValidation;
using LNSF.Domain;
using LNSF.Domain.Entities;

namespace LNSF.Application;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(room => room.Id)
            .NotEmpty();

        RuleFor(room => room.Available)
            .NotEmpty();
        
        RuleFor(room => room.Occupation)
            .NotEmpty();
        
        RuleFor(room => room.Storey)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(room => room.Bathroom)
            .NotEmpty();
        
        RuleFor(room => room.Number)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(room => room.Beds)
            .NotEmpty()
            .GreaterThan(0);
    }
}

public class RoomAddValidator : AbstractValidator<IRoomAdd>
{
    public RoomAddValidator()
    {
        RuleFor(room => room.Available)
            .NotEmpty();
        
        RuleFor(room => room.Occupation)
            .NotEmpty();
        
        RuleFor(room => room.Storey)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(room => room.Bathroom)
            .NotEmpty();
        
        RuleFor(room => room.Number)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(room => room.Beds)
            .NotEmpty()
            .GreaterThan(0);
    }
}
