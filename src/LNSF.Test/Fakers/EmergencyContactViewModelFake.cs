﻿using Bogus;
using LNSF.UI.ViewModels;

namespace LNSF.Test;

public class EmergencyContactViewModelFake : Faker<EmergencyContactPostViewModel>
{
    public EmergencyContactViewModelFake()
    {
        RuleFor(x => x.Name, f => f.Person.FullName);
        RuleFor(x => x.Phone, f => f.Random.ReplaceNumbers("(##) # ####-####"));
    }
}
