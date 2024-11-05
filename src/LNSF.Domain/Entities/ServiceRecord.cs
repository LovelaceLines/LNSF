using LNSF.Domain.Enums;

namespace LNSF.Domain.Entities;

public class ServiceRecord : BaseEntity
{
	public int PatientId { get; set; }
	public Patient? Patient { get; set; }


	public List<SocialProgram> SocialProgram { get; set; } = [];
	public required string SocialProgramNote { get; set; }


	public DomicileType DomicileType { get; set; }
	public List<MaterialExternalWallsDomicile> MaterialExternalWallsDomicile { get; set; } = [];
	public AccessElectricalEnergy AccessElectricalEnergy { get; set; }
	public bool HasWaterSupply { get; set; }
	public List<WayWaterSupply> WayWaterSupply { get; set; } = [];
	public SanitaryDrainage SanitaryDrainage { get; set; }
	public GarbageCollection GarbageCollection { get; set; }
	public int NumberRooms { get; set; }
	public int NumberBedrooms { get; set; }
	public int NumberPeoplePerBedroom { get; set; }
	public DomicileHasAccessibility DomicileHasAccessibility { get; set; }
	public bool IsLocatedInRiskArea { get; set; }
	public bool IsLocatedInDifficultAccessArea { get; set; }
	public bool IsLocatedInConflictViolenceArea { get; set; }


	public AccessToUnit AccessToUnit { get; set; }
	public required string AccessToUnitNote { get; set; }
	public required string FirstAttendanceReason { get; set; }

	public required string DemandPresented { get; set; }
	public required string Referrals { get; set; }
	public required string Observations { get; set; }
}
