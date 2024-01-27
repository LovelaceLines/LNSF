using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class HospitalPostViewModelFake : Faker<HospitalPostViewModel>
{
    public HospitalPostViewModelFake(string? name = null, string? acronym = null)
    {
        RuleFor(h => h.Name, f => name ?? f.Company.CompanyName() + f.Random.Number(1, 1000));
        RuleFor(h => h.Acronym, f => acronym ?? f.Company.CompanySuffix());
    }
}

public class HospitalViewModelFake : Faker<HospitalViewModel>
{
    public HospitalViewModelFake(int id, string? name = null, string? acronym = null)
    {
        RuleFor(h => h.Id, f => id);
        RuleFor(h => h.Name, f => name ?? f.Company.CompanyName() + f.Random.Number(1, 1000));
        RuleFor(h => h.Acronym, f => acronym ?? f.Company.CompanySuffix());
    }
}
