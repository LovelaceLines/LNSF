﻿namespace LNSF.Domain;

public interface IRoomAdd
{
    public int? Number { get; set; }
    public bool? Bathroom { get; set; } 
    public int? Beds { get; set; } 
    public int? Occupation { get; set; }
    public int? Storey { get; set; }
    public bool? Available { get; set; } 
}
