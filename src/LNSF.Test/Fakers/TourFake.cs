using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class TourFake : Faker<Tour>
{
	public TourFake(int? id = null, int? peopleId = null, DateTime? output = null, DateTime? input = null, string? note = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.PeopleId, f => peopleId ?? 0);
		RuleFor(x => x.Output, f => output ?? DateTime.Now);
		RuleFor(x => x.Input, f => input ?? DateTime.Now);
		RuleFor(x => x.Note, f => note ?? f.Lorem.Sentence(10));
	}
}
