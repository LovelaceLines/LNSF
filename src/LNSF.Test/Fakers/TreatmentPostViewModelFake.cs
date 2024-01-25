using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class TreatmentPostViewModelFake : Faker<TreatmentPostViewModel>
{
    public TreatmentPostViewModelFake(string? name = null, TypeTreatment? type = null)
    {
        RuleFor(x => x.Name, f => name ?? f.Name.JobTitle() + f.Random.Number(max: 1000));
        RuleFor(x => x.Type, f => type ?? f.PickRandom<TypeTreatment>());
    }
}

public class TreatmentViewModelFake : Faker<TreatmentViewModel>
{
    public TreatmentViewModelFake(int id, string? name = null, TypeTreatment? type = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.Name, f => name ?? f.Name.JobTitle());
        RuleFor(x => x.Type, f => type ?? f.PickRandom<TypeTreatment>());
    }
}