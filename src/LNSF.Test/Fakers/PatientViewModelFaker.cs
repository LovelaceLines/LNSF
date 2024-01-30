using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class PatientPostViewModelFake : Faker<PatientPostViewModel>
{
    public PatientPostViewModelFake(int peopleId, int hospitalId, bool? socioeconomicRecord = null, bool? term = null)
    {
        RuleFor(p => p.PeopleId, f => peopleId);
        RuleFor(p => p.HospitalId, f => hospitalId);
        RuleFor(p => p.SocioeconomicRecord, f => socioeconomicRecord ?? f.Random.Bool());
        RuleFor(p => p.Term, f => term ?? f.Random.Bool());
    }
}

public class PatientViewModelFake : Faker<PatientViewModel>
{
    public PatientViewModelFake(int id, int peopleId, int hospitalId, bool? socioeconomicRecord = null, bool? term = null)
    {
        RuleFor(p => p.Id, f => id);
        RuleFor(p => p.PeopleId, f => peopleId);
        RuleFor(p => p.HospitalId, f => hospitalId);
        RuleFor(p => p.SocioeconomicRecord, f => socioeconomicRecord ?? f.Random.Bool());
        RuleFor(p => p.Term, f => term ?? f.Random.Bool());
    }
}

public class PatientTreatmentViewModelFake : Faker<PatientTreatmentViewModel>
{
    public PatientTreatmentViewModelFake(int patientId, int treatmentId)
    {
        RuleFor(p => p.PatientId, f => patientId);
        RuleFor(p => p.TreatmentId, f => treatmentId);
    }
}
