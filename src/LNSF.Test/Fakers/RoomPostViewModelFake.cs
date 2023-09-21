using Bogus;
using LNSF.UI.ViewModels;

namespace LNSF.Test.Fakers;

public class RoomPostViewModelFake : Faker<RoomPostViewModel>
{
    public RoomPostViewModelFake()
    {
        RuleFor(r => r.Number, f => f.Address.BuildingNumber());
        RuleFor(r => r.Bathroom, f => f.Random.Bool());
        RuleFor(r => r.Beds, f => f.Random.Number(1, 4));
        RuleFor(r => r.Occupation, (f, r) => f.Random.Number(0, r.Beds));
        RuleFor(r => r.Storey, f => f.Random.Number(1, 2));
        RuleFor(r => r.Available, f => f.Random.Bool());
    }
}
