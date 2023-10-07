using FluentValidation;
using LNSF.Domain.Entities;

namespace LNSF.Application.Validators;

public class AuthenticationTokenValidator : AbstractValidator<AuthenticationToken>
{
    public AuthenticationTokenValidator()
    {
        RuleFor(token => token.AccessToken)
            .NotEmpty().WithMessage(GlobalValidator.RequiredField("Token de acesso"));
        
        RuleFor(token => token.RefreshToken)
            .NotEmpty().WithMessage(GlobalValidator.RequiredField("Token de atualização"));
    }
}
