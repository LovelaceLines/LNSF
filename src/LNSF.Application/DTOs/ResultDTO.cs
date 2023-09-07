namespace LNSF.Application;

public class ResultDTO<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Error { get; set; } = false;
}