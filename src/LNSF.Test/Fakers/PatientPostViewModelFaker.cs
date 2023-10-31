using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class PatientPostViewModelFake : Faker<PatientPostViewModel>
{
    public PatientPostViewModelFake()
    {
        RuleFor(p => p.Term, f => f.Random.Bool());
        RuleFor(p => p.SocioeconomicRecord, f => f.Random.Bool());
    }
}
