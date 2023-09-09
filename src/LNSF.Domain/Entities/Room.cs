namespace LNSF.Domain.Entities;

public class Room
{
    public int? Id { get; set; }
    public string Number { get; set; }
    public bool Bathroom { get; set; } 
    public int Beds { get; set; } 
    public int Occupation { get; set; }
    public int Storey { get; set; }
    public bool Available { get; set; }

    public Room(
        string number, 
        bool bathroom, 
        int beds, 
        int occupation, 
        int storey,
        bool available,
        int? id = null)
    {
        Id = id;
        Number = number;
        Bathroom = bathroom;
        Beds = beds;
        Occupation = occupation;
        Storey = storey;
        Available = available;
    }
}
