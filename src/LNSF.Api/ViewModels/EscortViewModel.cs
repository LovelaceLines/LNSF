namespace LNSF.Api.ViewModels;

public class EscortViewModel 
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public PeopleViewModel? People { get; set; } = null;
}

public class EscortPostViewModel 
{
    public int PeopleId { get; set; }
}