﻿using Bogus;
using LNSF.Api.ViewModels;
using LNSF.Domain.DTOs;

namespace LNSF.Test.Fakers;

public class HostingPostViewModelFake : Faker<HostingPostViewModel>
{
    public HostingPostViewModelFake()
    {
        RuleFor(x => x.CheckIn, f => f.Date.Past());
        RuleFor(x => x.CheckOut, f => f.Date.Future());
    }
}

public class HostingEscortInfoFake : Faker<HostingEscortInfo>
{
    public HostingEscortInfoFake()
    {
        RuleFor(x => x.CheckIn, f => f.Date.Past());
        RuleFor(x => x.CheckOut, f => f.Date.Future());
    }
}
