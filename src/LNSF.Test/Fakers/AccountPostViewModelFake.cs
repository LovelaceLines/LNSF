using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class AccountPostViewModelFake : Faker<AccountPostViewModel>
{
    public AccountPostViewModelFake()
    {
        RuleFor(x => x.UserName, f => f.Person.UserName);
        RuleFor(x => x.Password, f => f.Person.FirstName + "#" + f.Random.Replace("###"));
        RuleFor(x => x.Role, f => f.PickRandom<Role>());
    }
}
