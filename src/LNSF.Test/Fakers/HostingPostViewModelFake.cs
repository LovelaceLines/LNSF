using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class HostingPostViewModelFake : Faker<HostingPostViewModel>
{
    public HostingPostViewModelFake(int patientId, DateTime? checkIn = null, DateTime? checkOut = null)
    {
        RuleFor(x => x.PatientId, f => patientId);
        RuleFor(x => x.CheckIn, f => checkIn ?? f.Date.Past());
        RuleFor(x => x.CheckOut, f => checkOut ?? f.Date.Future());
    }
}

public class HostingViewModelFake : Faker<HostingViewModel>
{
    public HostingViewModelFake(int id, int? patientId = null, DateTime? checkIn = null, DateTime? checkOut = null)
    {
        RuleFor(x => x.Id, f => id);
        RuleFor(x => x.PatientId, f => patientId);
        RuleFor(x => x.CheckIn, f => checkIn ?? f.Date.Past());
        RuleFor(x => x.CheckOut, f => checkOut ?? f.Date.Future());
    }
}

public class HostingEscortViewModelFake : Faker<HostingEscortViewModel>
{
    public HostingEscortViewModelFake(int hostingId, int escortId)
    {
        RuleFor(x => x.HostingId, f => hostingId);
        RuleFor(x => x.EscortId, f => escortId);
    }
}
