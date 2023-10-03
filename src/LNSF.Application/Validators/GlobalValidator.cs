using FluentValidation;
using LNSF.Domain.DTOs;

namespace LNSF.Application.Validators;

public class GlobalValidator
{
    public static string RequiredField(string field) => $"{field}.Campo obrigatório.";
    public static string InvalidField(string field) => $"{field}. Campo inválido.";
    public static string MaxLength(string field, int length) => $"{field}.Tamanho/Valor máximo excedido. Máximo de {length} caracteres.";
    public static string MinLength(string field, int length) => $"{field}.Tamanho/Valor mínimo não atingido. Mínimo de {length} caracteres.";
    public static string InvalidRGFormat() => "RG inválido. Use o formato XX.XXX.XXX-X.";
    public static string InvalidCPFFormat() => "CPF inválido. Use o formato XXX.XXX.XXX-XX.";
    public static string InvalidAge() => "Idade inválida.";
    public static string InvalidDateTimeFormat() => "Data inválida. Use o formato dd/MM/yyyy HH:mm";
    public static string InvalidPhoneFormat() => "Telefone inválida. Use o formato (XX) X XXXX-XXXX)";
    public static string InvalidDateFormat() =>  "Data inválida. Use o formato dd/MM/yyyy";
}

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(x => x)
            .Matches(@"^\(\d{2}\) \d \d{4}-\d{4}$").WithMessage(GlobalValidator.InvalidPhoneFormat());
    }
}

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {   
        RuleFor(page => page.Page)
            .GreaterThan(0).WithMessage(GlobalValidator.MinLength("Página", 1));
        
        RuleFor(page => page.PageSize)
            .GreaterThan(0).WithMessage(GlobalValidator.MinLength("Tamanho da página", 1));
    }
}

public class OrderByValidator : AbstractValidator<OrderBy>
{
    public OrderByValidator()
    {
        RuleFor(x => x)
            .NotNull().WithMessage(GlobalValidator.RequiredField("Ordenação"))
            .IsInEnum().WithMessage(GlobalValidator.InvalidField("Ordenação"));
    }
}

public class DateValidator : AbstractValidator<DateTime>
{
    public DateValidator()
    {
        RuleFor(date => date.ToString("dd/MM/yyyy"))
            .Matches(@"^\d{2}/\d{2}/\d{4}$").WithMessage(GlobalValidator.InvalidDateFormat());
    }
}

public class DateTimeValidator : AbstractValidator<DateTime>
{
    public DateTimeValidator()
    {
        RuleFor(date => date.ToString("dd/MM/yyyy HH:mm:ss tt"))
            .Matches(@"^\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2} (AM|PM)$")
            .WithMessage(GlobalValidator.InvalidDateTimeFormat());
    }
}