﻿using Bogus;
using LNSF.Api.ViewModels;

namespace LNSF.Test.Fakers;

public class PeopleRoomHostingViewModelFake : Faker<PeopleRoomHostingViewModel>
{
    public PeopleRoomHostingViewModelFake(int roomId, int peopleId, int hostingId)
    {
        RuleFor(x => x.RoomId, f => roomId);
        RuleFor(x => x.PeopleId, f => peopleId);
        RuleFor(x => x.HostingId, f => hostingId);
    }
}
