using AutoFilterer.Attributes;

namespace LNSF.Domain.Filters;

public class RoomFilter : BaseFilter
{
	public int? Id { get; set; }
	[ToLowerContainsComparison]
	public string? Number { get; set; }
	public bool? Bathroom { get; set; }
	public int? Beds { get; set; }
	public int? Storey { get; set; }
	public bool? Available { get; set; }

	public bool? IsAvailable { get; set; }
	public DateTime? CheckIn { get; set; }
	public DateTime? CheckOut { get; set; }
}
