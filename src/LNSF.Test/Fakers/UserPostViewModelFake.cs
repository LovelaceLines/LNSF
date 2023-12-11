using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class UserPostViewModelFake : Faker<UserPostViewModel>
{
    public UserPostViewModelFake(string? userName = null, string? email = null, string? phoneNumber = null, string? password = null)
    {
        RuleFor(x => x.UserName, f => userName ?? f.Person.UserName);
        RuleFor(x => x.Email, f => email ?? f.Person.Email);
        RuleFor(x => x.PhoneNumber, f => phoneNumber ?? f.Random.Replace("(##) # ####-####"));
        RuleFor(x => x.Password, f => password ?? f.Person.FirstName + "#" + f.Random.Replace("###"));
    }
}

public class UserViewModelFake : Faker<UserViewModel>
{
    public UserViewModelFake(string id, string? userName = null, string? email = null, string? phoneNumber = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.UserName, f => userName ?? f.Person.UserName);
        RuleFor(x => x.Email, f => email ?? f.Person.Email);
        RuleFor(x => x.PhoneNumber, f => phoneNumber ?? f.Random.Replace("(##) # ####-####"));
    }
}

public class UserLoginViewModelFake : Faker<UserLoginViewModel>
{
    public UserLoginViewModelFake(string? userName = null, string? password = null)
    {
        RuleFor(x => x.UserName, f => userName ?? f.Person.UserName);
        RuleFor(x => x.Password, f => password ?? f.Person.FirstName + "#" + f.Random.Replace("###"));
    }
}
