namespace LNSF.Domain.Entities;

public class Tour
{
    public int? Id { get; set; }
    public DateTime Output { get; set; }
    public DateTime? Input { get; set; }
    public string? Note { get; set; }

    public Tour(
        DateTime output, 
        int? id = null,
        DateTime? input = null,
        string? note = null)
    {
        Id = id;
        Output = output;
        Input = input;
        Note = note;
    }
}
