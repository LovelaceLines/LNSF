using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class ServiceRecordViewModelFake : Faker<ServiceRecordViewModel>
{
    public ServiceRecordViewModelFake(int id, int patientId, List<SocialProgram>? socialProgram = null, string? socialProgramNote = null, DomicileType? domicileType = null, List<MaterialExternalWallsDomicile>? materialExternalWallsDomicile = null, AccessElectricalEnergy? accessElectricalEnergy = null, bool? hasWaterSupply = null, List<WayWaterSupply>? wayWaterSupply = null, SanitaryDrainage? sanitaryDrainage = null, GarbageCollection? garbageCollection = null, int? numberRooms = null, int? numberBedrooms = null, int? numberPeoplePerBedroom = null, DomicileHasAccessibility? domicileHasAccessibility = null, bool? isLocatedInRiskArea = null, bool? isLocatedInDifficultAccessArea = null, bool? isLocatedInConflictViolenceArea = null, AccessToUnit? accessToUnit = null, string? accessToUnitNote = null, string? firstAttendanceReason = null, string? demandPresented = null, string? referrals = null, string? observations = null)
    {
        RuleFor(r => r.Id, f => id);
        RuleFor(r => r.PatientId, f => patientId);
        RuleFor(r => r.SocialProgram, f => socialProgram ?? [f.PickRandom<SocialProgram>()]);
        RuleFor(r => r.SocialProgramNote, f => socialProgramNote ?? f.Lorem.Sentence());
        RuleFor(r => r.DomicileType, f => domicileType ?? f.PickRandom<DomicileType>());
        RuleFor(r => r.MaterialExternalWallsDomicile, f => materialExternalWallsDomicile ?? [f.PickRandom<MaterialExternalWallsDomicile>()]);
        RuleFor(r => r.AccessElectricalEnergy, f => accessElectricalEnergy ?? f.PickRandom<AccessElectricalEnergy>());
        RuleFor(r => r.HasWaterSupply, f => hasWaterSupply ?? f.Random.Bool());
        RuleFor(r => r.WayWaterSupply, f => wayWaterSupply ?? [f.PickRandom<WayWaterSupply>()]);
        RuleFor(r => r.SanitaryDrainage, f => sanitaryDrainage ?? f.PickRandom<SanitaryDrainage>());
        RuleFor(r => r.GarbageCollection, f => garbageCollection ?? f.PickRandom<GarbageCollection>());
        RuleFor(r => r.NumberRooms, f => numberRooms ?? f.Random.Number(1, 10));
        RuleFor(r => r.NumberBedrooms, f => numberBedrooms ?? f.Random.Number(1, numberRooms ?? 2));
        RuleFor(r => r.NumberPeoplePerBedroom, f => numberPeoplePerBedroom ?? f.Random.Number(1, 5));
        RuleFor(r => r.DomicileHasAccessibility, f => domicileHasAccessibility ?? f.PickRandom<DomicileHasAccessibility>());
        RuleFor(r => r.IsLocatedInRiskArea, f => isLocatedInRiskArea ?? f.Random.Bool());
        RuleFor(r => r.IsLocatedInDifficultAccessArea, f => isLocatedInDifficultAccessArea ?? f.Random.Bool());
        RuleFor(r => r.IsLocatedInConflictViolenceArea, f => isLocatedInConflictViolenceArea ?? f.Random.Bool());
        RuleFor(r => r.AccessToUnit, f => accessToUnit ?? f.PickRandom<AccessToUnit>());
        RuleFor(r => r.AccessToUnitNote, f => accessToUnitNote ?? f.Lorem.Sentence());
        RuleFor(r => r.FirstAttendanceReason, f => firstAttendanceReason ?? f.Lorem.Sentence());
        RuleFor(r => r.DemandPresented, f => demandPresented ?? f.Lorem.Sentence());
        RuleFor(r => r.Referrals, f => referrals ?? f.Lorem.Sentence());
        RuleFor(r => r.Observations, f => observations ?? f.Lorem.Sentence());
    }
}

public class ServiceRecordPostViewModelFake : Faker<ServiceRecordPostViewModel>
{
    public ServiceRecordPostViewModelFake(int patientId, List<SocialProgram>? socialProgram = null, string? socialProgramNote = null, DomicileType? domicileType = null, List<MaterialExternalWallsDomicile>? materialExternalWallsDomicile = null, AccessElectricalEnergy? accessElectricalEnergy = null, bool? hasWaterSupply = null, List<WayWaterSupply>? wayWaterSupply = null, SanitaryDrainage? sanitaryDrainage = null, GarbageCollection? garbageCollection = null, int? numberRooms = null, int? numberBedrooms = null, int? numberPeoplePerBedroom = null, DomicileHasAccessibility? domicileHasAccessibility = null, bool? isLocatedInRiskArea = null, bool? isLocatedInDifficultAccessArea = null, bool? isLocatedInConflictViolenceArea = null, AccessToUnit? accessToUnit = null, string? accessToUnitNote = null, string? firstAttendanceReason = null, string? demandPresented = null, string? referrals = null, string? observations = null)
    {
        RuleFor(r => r.PatientId, f => patientId);
        RuleFor(r => r.SocialProgram, f => socialProgram ?? [f.PickRandom<SocialProgram>()]);
        RuleFor(r => r.SocialProgramNote, f => socialProgramNote ?? f.Lorem.Sentence());
        RuleFor(r => r.DomicileType, f => domicileType ?? f.PickRandom<DomicileType>());
        RuleFor(r => r.MaterialExternalWallsDomicile, f => materialExternalWallsDomicile ?? [f.PickRandom<MaterialExternalWallsDomicile>()]);
        RuleFor(r => r.AccessElectricalEnergy, f => accessElectricalEnergy ?? f.PickRandom<AccessElectricalEnergy>());
        RuleFor(r => r.HasWaterSupply, f => hasWaterSupply ?? f.Random.Bool());
        RuleFor(r => r.WayWaterSupply, f => wayWaterSupply ?? [f.PickRandom<WayWaterSupply>()]);
        RuleFor(r => r.SanitaryDrainage, f => sanitaryDrainage ?? f.PickRandom<SanitaryDrainage>());
        RuleFor(r => r.GarbageCollection, f => garbageCollection ?? f.PickRandom<GarbageCollection>());
        RuleFor(r => r.NumberRooms, f => numberRooms ?? f.Random.Number(1, 10));
        RuleFor(r => r.NumberBedrooms, f => numberBedrooms ?? f.Random.Number(1, numberRooms ?? 2));
        RuleFor(r => r.NumberPeoplePerBedroom, f => numberPeoplePerBedroom ?? f.Random.Number(1, 5));
        RuleFor(r => r.DomicileHasAccessibility, f => domicileHasAccessibility ?? f.PickRandom<DomicileHasAccessibility>());
        RuleFor(r => r.IsLocatedInRiskArea, f => isLocatedInRiskArea ?? f.Random.Bool());
        RuleFor(r => r.IsLocatedInDifficultAccessArea, f => isLocatedInDifficultAccessArea ?? f.Random.Bool());
        RuleFor(r => r.IsLocatedInConflictViolenceArea, f => isLocatedInConflictViolenceArea ?? f.Random.Bool());
        RuleFor(r => r.AccessToUnit, f => accessToUnit ?? f.PickRandom<AccessToUnit>());
        RuleFor(r => r.AccessToUnitNote, f => accessToUnitNote ?? f.Lorem.Sentence());
        RuleFor(r => r.FirstAttendanceReason, f => firstAttendanceReason ?? f.Lorem.Sentence());
        RuleFor(r => r.DemandPresented, f => demandPresented ?? f.Lorem.Sentence());
        RuleFor(r => r.Referrals, f => referrals ?? f.Lorem.Sentence());
        RuleFor(r => r.Observations, f => observations ?? f.Lorem.Sentence());
    }
}
