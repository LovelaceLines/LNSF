using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class RoomPostViewModelFake : Faker<RoomPostViewModel>
{
    public RoomPostViewModelFake(bool? available = null, 
        string? number = null, 
        int? beds = null, 
        int? storey = null,
        bool? bathroom = null)
    {
        RuleFor(r => r.Available, f => available ?? f.Random.Bool());
        RuleFor(r => r.Number, f => number ?? f.Address.BuildingNumber());
        RuleFor(r => r.Beds, f => beds ?? f.Random.Number(1, 4));
        RuleFor(r => r.Storey, f => storey ?? f.Random.Number(1, 2));
        RuleFor(r => r.Bathroom, f => bathroom ?? f.Random.Bool());
    }
}

public class RoomViewModelFake : Faker<RoomViewModel>
{
    public RoomViewModelFake(int id, 
        bool? available = null, 
        string? number = null, 
        int? beds = null, 
        int? storey = null,
        bool? bathroom = null)
    {
        RuleFor(r => r.Id, f => id);
        RuleFor(r => r.Available, f => available ?? f.Random.Bool());
        RuleFor(r => r.Number, f => number ?? f.Address.BuildingNumber());
        RuleFor(r => r.Beds, f => beds ?? f.Random.Number(1, 4));
        RuleFor(r => r.Storey, f => storey ?? f.Random.Number(1, 2));
        RuleFor(r => r.Bathroom, f => bathroom ?? f.Random.Bool());
    }
}
