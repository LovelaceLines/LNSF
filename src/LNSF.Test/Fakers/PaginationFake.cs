using LNSF.Domain.Filters;
using Bogus;

namespace LNSF.Test.Fakers;

public class PaginationFake : Faker<Pagination>
{
    public PaginationFake()
    {
        RuleFor(p => p.Page, f => 1);
        RuleFor(p => p.PageSize, f => f.Random.Int(1, 9999));
    }
}
