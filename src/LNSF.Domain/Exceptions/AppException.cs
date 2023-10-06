using System.Net;

namespace LNSF.Domain.Exceptions;

public class AppException : Exception
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; set; } = DateTime.Now;
    public int StatusCode { get; set; } = 500;
    public override string Message { get; }
    
    public AppException(string message) : base(message) => 
        Message = message;
    
    public AppException(string message, int statusCode) : base(message) =>
        (Message, StatusCode) = (message, statusCode);

    public AppException(string message, HttpStatusCode statusCode) : this(message) => 
        (Message, StatusCode) = (message, (int)statusCode);
}
