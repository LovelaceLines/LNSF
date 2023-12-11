using System.Globalization;
using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class TourViewModelFake : Faker<TourViewModel>
{
    public TourViewModelFake(int id, int peopleId, DateTime? output = null, DateTime? input = null, string? note = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.Output, f => output ?? DateTime.ParseExact(f.Date.Past().ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
        RuleFor(x => x.Input, f => input ?? DateTime.ParseExact(f.Date.Future().ToString("dd/MM/yyyy HH:mm"), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
        RuleFor(x => x.Note, f => note ?? f.Lorem.Sentence(10));
    }
}

public class TourPostViewModelFake : Faker<TourPostViewModel>
{
    public TourPostViewModelFake(int peopleId, string? note = null)
    {
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.Note, f => note ?? f.Lorem.Sentence(10));
    }
}

public class TourPutViewModelFake : Faker<TourPutViewModel>
{
    public TourPutViewModelFake(int id, int peopleId, string? note = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.Note, f => note ?? f.Lorem.Sentence(10));
    }
}