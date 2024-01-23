using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class RoomFilter
{
    public int? Id { get; set; }
    public string? Number { get; set; }
    public bool? Bathroom { get; set; }
    public int? Beds { get; set; }
    public int? Storey { get; set; }
    public bool? Available { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public RoomFilter() { }

    public RoomFilter(int? id = null,
        string? number = null,
        bool? bathroom = null,
        int? beds = null,
        int? storey = null,
        bool? available = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Number = number;
        Bathroom = bathroom;
        Beds = beds;
        Storey = storey;
        Available = available;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}