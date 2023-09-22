using System.Globalization;

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
    public static string InvalidDateTimeFormat() => "Data inválida. Use o formato dd/MM/yyyy hh:mm";
    public static string InvalidPhoneFormat() => "Telefone inválida. Use o formato (XX) X XXXX-XXXX)";
    public static string InvalidDateFormat() =>  "Data inválida. Use o formato dd/MM/yyyy";
}
