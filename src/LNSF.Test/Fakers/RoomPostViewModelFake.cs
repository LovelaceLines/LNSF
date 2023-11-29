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
        if (number == null) RuleFor(r => r.Number, f => f.Address.BuildingNumber());
        else RuleFor(r => r.Number, f => number);

        if (available.HasValue) RuleFor(r => r.Available, f => available);
        else RuleFor(r => r.Available, f => f.Random.Bool());

        if (beds.HasValue) RuleFor(r => r.Beds, f => beds);
        else RuleFor(r => r.Beds, f => f.Random.Number(1, 4));

        if (storey.HasValue) RuleFor(r => r.Storey, f => storey);
        else RuleFor(r => r.Storey, f => f.Random.Number(1, 2));

        if (bathroom.HasValue) RuleFor(r => r.Bathroom, f => bathroom);
        else RuleFor(r => r.Bathroom, f => f.Random.Bool());
    }
}

public class RoomPutViewModelFake : Faker<RoomViewModel>
{
    public RoomPutViewModelFake(int id, 
        bool? available = null, 
        string? number = null, 
        int? beds = null, 
        int? storey = null,
        bool? bathroom = null)
    {
        RuleFor(r => r.Id, f => id);

        if (number == null) RuleFor(r => r.Number, f => f.Address.BuildingNumber());
        else RuleFor(r => r.Number, f => number);

        if (available.HasValue) RuleFor(r => r.Available, f => available);
        else RuleFor(r => r.Available, f => f.Random.Bool());

        if (beds.HasValue) RuleFor(r => r.Beds, f => beds);
        else RuleFor(r => r.Beds, f => f.Random.Number(1, 4));

        if (storey.HasValue) RuleFor(r => r.Storey, f => storey);
        else RuleFor(r => r.Storey, f => f.Random.Number(1, 2));

        if (bathroom.HasValue) RuleFor(r => r.Bathroom, f => bathroom);
        else RuleFor(r => r.Bathroom, f => f.Random.Bool());
    }
}
