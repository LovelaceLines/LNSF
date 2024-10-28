namespace LNSF.Domain.Entities;

public class ChainCountPeopleHostedFilter
{
	public DateOnly Date { get; set; }
}

public class ChainDayFilter
{
	public int Days { get; set; }
}

public class TreatmentDTO : Treatment
{
	public int TotalCount { get; set; }
}

public class ChainIntervalCheckFilter
{
	public DateOnly CheckIn { get; set; }
	public DateOnly CheckOut { get; set; }
}
