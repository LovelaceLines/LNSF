using Bogus;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class HostingFake : Faker<Hosting>
{
	public HostingFake(int? id = null, int? patientId = null, DateTime? checkIn = null, DateTime? checkOut = null)
	{
		RuleFor(x => x.Id, f => id ?? 0);
		RuleFor(x => x.PatientId, f => patientId ?? 0);
		RuleFor(x => x.CheckIn, f => checkIn ?? f.Date.Past());
		RuleFor(x => x.CheckOut, f => checkOut ?? f.Date.Future());
	}
}
