using System.Globalization;
using Bogus;
using LNSF.UI.ViewModels;

namespace LNSF.Test.Fakers;

public class TourViewModelFake : Faker<TourViewModel>
{
    public TourViewModelFake()
    {
        RuleFor(x => x.Output, f => DateTime.ParseExact(f.Date.Past().ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
        RuleFor(x => x.Input, f => DateTime.ParseExact(f.Date.Future().ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
        RuleFor(x => x.Note, f => f.Lorem.Sentence(10));
    }
}
