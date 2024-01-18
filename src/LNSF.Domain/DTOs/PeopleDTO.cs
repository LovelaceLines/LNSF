using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class PeopleDTO : People
{
    public string? Experience { get; set; } = null;
    public string? Status { get; set; } = null;

    public PeopleDTO() { }

    public PeopleDTO(People people)
    {
        Id = people.Id;
        Name = people.Name;
        Gender = people.Gender;
        BirthDate = people.BirthDate;
        RG = people.RG;
        IssuingBody = people.IssuingBody;
        CPF = people.CPF;
        Street = people.Street;
        HouseNumber = people.HouseNumber;
        Neighborhood = people.Neighborhood;
        City = people.City;
        State = people.State;
        Phone = people.Phone;
        Note = people.Note;
    }
}
