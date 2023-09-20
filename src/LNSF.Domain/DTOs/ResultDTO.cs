namespace LNSF.Domain.DTOs;

public class ResultDTO<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = "";
    public bool Success { get; set; }

    public ResultDTO()
    {
        Success = true;
    }

    public ResultDTO(T data)
    {
        Data = data;
        Success = false;
    }

    public ResultDTO(string message)
    {
        Message = message;
        Success = true;
    }
}