namespace LNSF.Domain.Filters;

public class RoomFilter : BaseFilter
{
	public int? Id { get; set; }
	public string? Number { get; set; }
	public bool? Bathroom { get; set; }
	public int? Beds { get; set; }
	public int? Storey { get; set; }
	public bool? Available { get; set; }
}
