using Bogus;
using LNSF.Domain.DTOs;

namespace LNSF.Test.Fakers;

public class RoomFiltersFake : Faker<RoomFilters>
{
    public RoomFiltersFake()
    {
        RuleFor(r => r.Bathroom, f => f.Random.Bool());
        RuleFor(r => r.Beds, f => f.Random.Int(1, 4));
        RuleFor(r => r.Vacant, f => f.Random.Bool());
        RuleFor(r => r.Storey, f => f.Random.Int(1, 2));
        RuleFor(r => r.Available, f => f.Random.Bool());
        RuleFor(r => r.Page, f => new PaginationFake().Generate());
        RuleFor(r => r.Order, f => f.PickRandom<OrderBy>());    
    }
}
