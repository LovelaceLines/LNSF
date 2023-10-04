namespace LNSF.Domain.Filters;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
}
