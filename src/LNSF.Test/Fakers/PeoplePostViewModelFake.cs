using System.Globalization;
using Bogus;
using Bogus.Extensions.Brazil;
using LNSF.Domain.Enums;
using LNSF.UI.ViewModels;

namespace LNSF.Test.Fakers;

public class PeoplePostViewModelFake : Faker<PeoplePostViewModel>
{
    public PeoplePostViewModelFake()
    {
        RuleFor(p => p.Name, f => f.Person.FullName);
        RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        RuleFor(p => p.BirthDate, f => DateTime.ParseExact(f.Person.DateOfBirth.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture));
        RuleFor(p => p.RG, f => f.Random.ReplaceNumbers("##.###.###-#"));
        RuleFor(p => p.CPF, f => f.Person.Cpf());
        RuleFor(p => p.Street, f => f.Address.StreetName());
        RuleFor(p => p.HouseNumber, f => f.Address.BuildingNumber());
        RuleFor(p => p.Neighborhood, f => f.Address.State());
        RuleFor(p => p.City, f => f.Address.City());
        RuleFor(p => p.State, f => f.Address.State());
        RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(##) # ####-####"));
        RuleFor(p => p.Note, f => f.Lorem.Sentence(10));
    }
}
