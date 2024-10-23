using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class PatientFake : Faker<Patient>
{
	public PatientFake(int? id = null, int? peopleId = null, int? hospitalId = null, bool? socioeconomicRecord = null, bool? term = null)
	{
		RuleFor(p => p.Id, f => id ?? 0);
		RuleFor(p => p.PeopleId, f => peopleId ?? 0);
		RuleFor(p => p.HospitalId, f => hospitalId ?? 0);
		RuleFor(p => p.SocioeconomicRecord, f => socioeconomicRecord ?? f.Random.Bool());
		RuleFor(p => p.Term, f => term ?? f.Random.Bool());
	}
}
