using System.Globalization;

namespace LNSF.Application.Services.Validators;

public class GlobalValidator
{
    public bool IsDateFormatted(DateTime date)
    {
        return DateTime.TryParse(
            s: date.ToString("dd/MM/yyyy HH:mm:ss"),
            provider: CultureInfo.CreateSpecificCulture("pt-BR"),
            out _);
    }
}
