namespace LNSF.UI.ViewModels;

public class TourViewModel
{
    public int Id { get; set; }
    public DateTime Output { get; set; }
    public DateTime? Input { get; set; }
    public string Note { get; set; } = "";

    public int PeopleId { get; set; }
}

public class TourPostViewModel
{
    public string Note { get; set; } = "";
    public int PeopleId { get; set; }
}

public class TourPutViewModel
{
    public int Id { get; set; }
    public string Note { get; set; } = "";

    public int PeopleId { get; set; }
}
