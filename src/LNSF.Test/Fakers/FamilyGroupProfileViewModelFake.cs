using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Entities;

namespace LNSF.Test.Fakers;

public class FamilyGroupProfileViewModelFake : Faker<FamilyGroupProfileViewModel>
{
    public FamilyGroupProfileViewModelFake(int id, int patientId, string? name = null, string? kinship = null, int? age = null, string? profession = null, double? income = null)
    {
        RuleFor(f => f.Id, f => id);
        RuleFor(f => f.PatientId, f => patientId);
        RuleFor(f => f.Name, f => name ?? f.Name.FullName());
        RuleFor(f => f.Kinship, f => kinship ?? f.Music.Genre());
        RuleFor(f => f.Age, f => age ?? f.Random.Int(0, 120));
        RuleFor(f => f.Profession, f => profession ?? f.Name.JobTitle());
        RuleFor(f => f.Income, f => income ?? f.Random.Double(0, 30000));
    }
}

public class FamilyGroupProfilePostViewModelFake : Faker<FamilyGroupProfilePostViewModel>
{
    public FamilyGroupProfilePostViewModelFake(int patientId, string? name = null, string? kinship = null, int? age = null, string? profession = null, double? income = null)
    {
        RuleFor(f => f.PatientId, f => patientId);
        RuleFor(f => f.Name, f => name ?? f.Name.FullName());
        RuleFor(f => f.Kinship, f => kinship ?? f.Music.Genre());
        RuleFor(f => f.Age, f => age ?? f.Random.Int(0, 120));
        RuleFor(f => f.Profession, f => profession ?? f.Name.JobTitle());
        RuleFor(f => f.Income, f => income ?? f.Random.Double(0, 30000));
    }
}
