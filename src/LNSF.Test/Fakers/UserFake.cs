using Bogus;

namespace LNSF.Test.Fakers;

public class User : LNSF.API.InputModels.UserAndPassword { }

public class UserFake : Faker<User>
{
	public UserFake(int? id = null, string? userName = null, string? name = null, string? email = null, string? phoneNumber = null, string? password = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.UserName, f => userName ?? f.Person.UserName + f.Random.Replace("##**"));
		RuleFor(x => x.Name, f => name ?? f.Person.FullName);
		RuleFor(x => x.Email, f => email ?? f.Person.Email);
		RuleFor(x => x.PhoneNumber, f => phoneNumber ?? f.Person.Phone);
		RuleFor(x => x.Password, f => password ?? f.Person.FirstName + '@' + f.Random.Replace("##**"));
	}
}
