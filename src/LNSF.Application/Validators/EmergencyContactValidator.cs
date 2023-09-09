using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class EmergencyContactValidator : AbstractValidator<EmergencyContact>
{
    public EmergencyContactValidator()
    {
    }
}
