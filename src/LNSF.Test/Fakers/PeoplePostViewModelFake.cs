using Bogus;
using Bogus.Extensions.Brazil;
using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;

namespace LNSF.Test.Fakers;

public class PeoplePostViewModelFake : Faker<PeoplePostViewModel>
{
    public PeoplePostViewModelFake(string? name = null, Gender? gender = null, DateTime? birthDate = null, string? rg = null, string? issuingBody = null, string? cpf = null, string? street = null, string? houseNumber = null, string? neighborhood = null, string? city = null, string? state = null, string? phone = null, string? note = null)
    {
        RuleFor(p => p.Name, f => name ?? f.Person.FullName);
        RuleFor(p => p.Gender, f => gender ?? f.PickRandom<Gender>());
        RuleFor(p => p.BirthDate, f => birthDate ?? f.Person.DateOfBirth);
        RuleFor(p => p.RG, f => rg ?? f.Random.ReplaceNumbers("##.###.###-#"));
        RuleFor(p => p.IssuingBody, f => issuingBody ?? f.Random.Replace("?????").ToUpper() + "-" + f.Random.Replace("??").ToUpper());
        RuleFor(p => p.CPF, f => cpf ?? f.Person.Cpf());
        RuleFor(p => p.Street, f => street ?? f.Address.StreetName());
        RuleFor(p => p.HouseNumber, f => houseNumber ?? f.Address.BuildingNumber());
        RuleFor(p => p.Neighborhood, f => neighborhood ?? f.Address.State());
        RuleFor(p => p.City, f => city ?? f.Address.City());
        RuleFor(p => p.State, f => state ?? f.Address.State());
        RuleFor(p => p.Phone, f => phone ?? f.Phone.PhoneNumber("(##) #####-####"));
        RuleFor(p => p.Note, f => note ?? f.Lorem.Sentence(10));
    }
}

public class PeopleViewModelFake : Faker<PeopleViewModel>
{
    public PeopleViewModelFake(int id, string? name = null, Gender? gender = null, DateTime? birthDate = null, string? rg = null, string? cpf = null, string? street = null, string? houseNumber = null, string? neighborhood = null, string? city = null, string? state = null, string? phone = null, string? note = null)
    {
        RuleFor(p => p.Id, f => id);
        RuleFor(p => p.Name, f => name ?? f.Person.FullName);
        RuleFor(p => p.Gender, f => gender ?? f.PickRandom<Gender>());
        RuleFor(p => p.BirthDate, f => birthDate ?? f.Person.DateOfBirth);
        RuleFor(p => p.RG, f => rg ?? f.Random.ReplaceNumbers("##.###.###-#"));
        RuleFor(p => p.IssuingBody, f => f.Random.Replace("?????").ToUpper() + "-" + f.Random.Replace("??").ToUpper());
        RuleFor(p => p.CPF, f => cpf ?? f.Person.Cpf());
        RuleFor(p => p.Street, f => street ?? f.Address.StreetName());
        RuleFor(p => p.HouseNumber, f => houseNumber ?? f.Address.BuildingNumber());
        RuleFor(p => p.Neighborhood, f => neighborhood ?? f.Address.State());
        RuleFor(p => p.City, f => city ?? f.Address.City());
        RuleFor(p => p.State, f => state ?? f.Address.State());
        RuleFor(p => p.Phone, f => phone ?? f.Phone.PhoneNumber("(##) #####-####"));
        RuleFor(p => p.Note, f => note ?? f.Lorem.Sentence(10));
    }
}
