﻿namespace LNSF.Api.ViewModels;

public class EmergencyContactViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public int PeopleId { get; set; }
    public PeopleViewModel? People { get; set; } = null;
}

public class EmergencyContactPostViewModel
{
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";

    public int PeopleId { get; set; }
}
