using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class HospitalFake : Faker<Hospital>
{
	public HospitalFake(int? id = null, string? name = null, string? acronym = null)
	{
		RuleFor(h => h.Id, f => id ?? 0);
		RuleFor(h => h.Name, f => name ?? f.Company.CompanyName() + f.Random.Number(1, 1000));
		RuleFor(h => h.Acronym, f => acronym ?? f.Company.CompanySuffix());
	}
}
