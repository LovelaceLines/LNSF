using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class RolePostViewModelFake : Faker<RolePostViewModel>
{
    public RolePostViewModelFake(string? name = null)
    {
        RuleFor(x => x.Name, f => name ?? f.Lorem.Word());
    }
}

public class RoleViewModelFake : Faker<RoleViewModel>
{
    public RoleViewModelFake(string id, string? name = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.Name, f => name ?? f.Lorem.Word());
    }
}
