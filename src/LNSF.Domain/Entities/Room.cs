namespace LNSF.Domain.Entities;

public class Room : IRoomAdd
{
    public int? Id { get; set; }
    public int? Number { get; set; }
    public bool? Bathroom { get; set; } 
    public int? Beds { get; set; } 
    public int? Occupation { get; set; }
    public int? Storey { get; set; }
    public bool? Available { get; set; }

    public Room(int? id = null, 
        int? number = null, 
        bool? bathroom = null, 
        int? beds = null, 
        int? occupation = 0, 
        int? storey = null,
        bool? available = false)
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
