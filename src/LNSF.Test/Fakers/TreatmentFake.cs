using Bogus;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class TreatmentFake : Faker<Treatment>
{
	public TreatmentFake(int? id = null, string? name = null, TypeTreatment? type = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.Name, f => name ?? f.Name.JobTitle() + f.Random.Number(max: 1000));
		RuleFor(x => x.Type, f => type ?? f.PickRandom<TypeTreatment>());
	}
}
