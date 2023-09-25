using Bogus;
using Bogus.Extensions.Brazil;
using LNSF.Domain.DTOs;

namespace LNSF.Test.Fakers;

public class PeopleFiltersFake : Faker<PeopleFilters>
{
    public PeopleFiltersFake()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, 9999));
        RuleFor(p => p.Name, f => f.Person.FullName);
        RuleFor(p => p.RG, f => f.Random.ReplaceNumbers("##.###.###.###-#"));
        RuleFor(p => p.CPF, f => f.Person.Cpf());
        RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(##) # ####-####"));
        RuleFor(p => p.Page, f => new PaginationFake().Generate());
    }
}
