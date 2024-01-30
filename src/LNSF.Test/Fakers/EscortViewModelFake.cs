using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class EscortPostViewModelFake : Faker<EscortPostViewModel>
{
    public EscortPostViewModelFake(int peopleId)
    {
        RuleFor(x => x.PeopleId, f => peopleId);
    }
}

public class EscortViewModelFake : Faker<EscortViewModel>
{
    public EscortViewModelFake(int id, int peopleId)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.PeopleId, f => peopleId);
    }
}
