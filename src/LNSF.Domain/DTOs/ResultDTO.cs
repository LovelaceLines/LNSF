namespace LNSF.Domain.DTOs;

public class ResultDTO<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public bool Error { get; set; }

    public ResultDTO(T data)
    {
        Data = data;
        Error = false;
    }

    public ResultDTO(string message)
    {
        Message = message;
        Error = true;
    }
}