﻿namespace LNSF.Domain.Entities;

public class Toor
{
    public int? Id { get; set; }
    public DateTime? Output { get; set; }
    public DateTime? Input { get; set; }
    public string? Note { get; set; }

    public Toor(
        int? id = null, 
        DateTime? output = null, 
        DateTime? input = null,
        string? note = null)
    {
        Id = id;
        Output = output;
        Note = note;
        Input = input;
    }
}
