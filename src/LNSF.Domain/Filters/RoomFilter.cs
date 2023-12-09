using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class RoomFilter
{
    public int? Id { get; set; }
    public string? Number { get; set; }
    public bool? Bathroom { get; set; }
    public int? Beds { get; set; }
    public bool? Vacant { get; set; }
    public int? Storey { get; set; }
    public bool? Available { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy Order { get; set; } = OrderBy.Ascending;
}