using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class RoomFake : Faker<Room>
{
	public RoomFake(int? id = null, bool? available = null, string? number = null, int? beds = null, int? storey = null, bool? bathroom = null)
	{
		RuleFor(r => r.Id, f => id ?? 0);
		RuleFor(r => r.Available, f => available ?? f.Random.Bool());
		RuleFor(r => r.Number, f => number ?? f.Random.Number(1, 99999999).ToString());
		RuleFor(r => r.Beds, f => beds ?? f.Random.Number(1, 4));
		RuleFor(r => r.Storey, f => storey ?? f.Random.Number(1, 2));
		RuleFor(r => r.Bathroom, f => bathroom ?? f.Random.Bool());
	}
}
