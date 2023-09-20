using LNSF.Domain.Enums;

namespace LNSF.UI.ViewModels;

public class PeoplePostViewModel
{
    public string Name { get; set; }  = "";
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string RG { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Street { get; set; } = "";
    public string HouseNumber { get; set; }  = "";
    public string Neighborhood { get; set; }  = "";
    public string City { get; set; }  = "";
    public string State { get; set; }  = "";
    public string Phone { get; set; }  = "";
    public string Note { get; set; }  = "";
}
