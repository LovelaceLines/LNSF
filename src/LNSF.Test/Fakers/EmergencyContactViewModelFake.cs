using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class EmergencyContactPostViewModelFake : Faker<EmergencyContactPostViewModel>
{
    public EmergencyContactPostViewModelFake(int peopleId, string? name = null, string? phone = null)
    {
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.Name, f => name ?? f.Person.FullName);
        RuleFor(x => x.Phone, f => phone ?? f.Random.ReplaceNumbers("(##) # ####-####"));
    }
}

public class EmergencyContactViewModelFake : Faker<EmergencyContactViewModel>
{
    public EmergencyContactViewModelFake(int id, int peopleId, string? name = null, string? phone = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.Name, f => name ?? f.Person.FullName);
        RuleFor(x => x.Phone, f => phone ?? f.Random.ReplaceNumbers("(##) # ####-####"));
    }
}
