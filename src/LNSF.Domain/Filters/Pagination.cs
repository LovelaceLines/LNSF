namespace LNSF.Domain.Filters;

public class Pagination
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;

    public Pagination() { }

    public Pagination(int page) =>
        Page = page;

    public Pagination(int page, int pageSize) =>
        (Page, PageSize) = (page, pageSize);
}
