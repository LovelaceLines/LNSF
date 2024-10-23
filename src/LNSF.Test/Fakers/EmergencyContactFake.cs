using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class EmergencyContactFake : Faker<EmergencyContact>
{
	public EmergencyContactFake(int? id = null, int? peopleId = null, string? name = null, string? phone = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.PeopleId, f => peopleId ?? 0);
		RuleFor(x => x.Name, f => name ?? f.Person.FullName);
		RuleFor(x => x.Phone, f => phone ?? f.Random.ReplaceNumbers("(##) #####-####"));
	}
}
