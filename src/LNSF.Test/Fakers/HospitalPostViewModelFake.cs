using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test;

public class HospitalPostViewModelFake : Faker<HospitalPostViewModel>
{  
    public HospitalPostViewModelFake()
    {
        RuleFor(h => h.Name, f => f.Company.CompanyName());
        RuleFor(h => h.Acronym, f => f.Random.ReplaceNumbers("####"));
    }
}
