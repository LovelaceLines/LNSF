using System.Globalization;
using LNSF.Domain.DTOs;

namespace LNSF.Application.Services.Validators;

public class GlobalValidator
{
    public const string RequiredField = "Campo obrigatório.";
    public const string MaxLength = "Tamanho máximo excedido.";
    public const string MinLength = "Tamanho mínimo não atingido.";
    public const string InvalidRGFormat = "RG inválido. Use o formato XX.XXX.XXX-X.";
    public const string InvalidCPFFormat = "CPF inválido. Use o formato XXX.XXX.XXX-XX.";
    public const string InvalidAge = "Idade inválida.";
    public const string InvalidDateFormat = "Data inválida. Use o formato dd/MM/yyyy";
    public const string InvalidDateTimeFormat = "Data inválida. Use o formato dd/MM/yyyy hh/mm";
    public const string InvalidPhoneFormat = "Telefone inválida. Use o formato (XX) X XXXX-XXXX)";
    public const string InvalidOutputDate = "Data de retorno deve ser maior que a data de saída.";
    public const string InvalidField = "Campo inválido.";

    public bool IsDateFormatted(DateTime? date)
    {   
        if (date == null) return false;

        DateTime _date = (DateTime)date;
        
        return DateTime.TryParse(
            s: _date.ToString("dd/MM/yyyy"),
            provider: CultureInfo.CreateSpecificCulture("pt-BR"),
            out _);
    }

    public bool IsGreater(DateTime? date1, DateTime? date2)
    {
        if (date1 == null || date2 == null)
        {
            return false;
        }

        DateTime _date1 = (DateTime)date1;
        DateTime _date2 = (DateTime)date2;

        return _date1 > _date2;
    }
}
