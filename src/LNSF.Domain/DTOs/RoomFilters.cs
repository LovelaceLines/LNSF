namespace LNSF.Domain.DTOs;

public class RoomFilters
{
    public bool Bathroom { get; set; }
    public int Beds { get; set; }
    public bool Vacant { get; set; }
    public int Storey { get; set; }
    public bool Available { get; set; }

    public Pagination Page { get; set; }
    public OrderBy OrderBy { get; set; }
}