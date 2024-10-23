using Bogus;

using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class RoleFake : Faker<Role>
{
	public RoleFake(int? id = null, string? name = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.Name, f =>
		{
			if (name != null) return name;

			var random = f.Random.Replace("##**");
			var companyName = name ?? f.Company.CompanyName();
			return companyName.Length > 28 ? companyName[..28] + random : companyName + random;
		});
	}
}
