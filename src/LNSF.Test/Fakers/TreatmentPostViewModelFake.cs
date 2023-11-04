using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class TreatmentPostViewModelFake : Faker<TreatmentPostViewModel>
{
    public TreatmentPostViewModelFake()
    {
        RuleFor(x => x.Name, f => f.Name.JobTitle());
        RuleFor(x => x.Type, f => f.PickRandom<TypeTreatment>());
    }
}