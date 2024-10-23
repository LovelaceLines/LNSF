using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class EscortFake : Faker<Escort>
{
	public EscortFake(int? id = null, int? peopleId = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.PeopleId, f => peopleId ?? 0);
	}
}
