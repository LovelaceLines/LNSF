using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class AccountPostViewModelFake : Faker<AccountPostViewModel>
{
    public AccountPostViewModelFake()
    {
        RuleFor(x => x.UserName, f => f.Person.UserName);
        RuleFor(x => x.Password, f => f.Random.ReplaceNumbers("######"));
        RuleFor(x => x.Role, f => Role.ADMINISTRATION);
    }
}
