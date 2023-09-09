using System.Globalization;

namespace LNSF.Application.Services.Validators;

public class GlobalValidator
{
    public bool IsDateFormatted(DateTime? date)
    {   
        if (date == null)
        {
            return false;
        }

        DateTime _date = (DateTime)date;
        
        return DateTime.TryParse(
            s: _date.ToString("dd/MM/yyyy HH:mm:ss"),
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
