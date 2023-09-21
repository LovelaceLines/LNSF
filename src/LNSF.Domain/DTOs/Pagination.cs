namespace LNSF.Domain.DTOs;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
}
