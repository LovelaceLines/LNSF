using System.Net;
using System.Text.Json.Serialization;

namespace LNSF.Domain.Exceptions;

public class AppException : Exception
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; set; } = DateTime.Now;
    public int StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;
    public override string Message { get; } = "An error occurred!";
    
    public AppException() : base() {}

    public AppException(string message) : base(message) => 
        Message = message;
    
    public AppException(string message, int statusCode) : base(message) =>
        (Message, StatusCode) = (message, statusCode);

    public AppException(string message, HttpStatusCode statusCode) : this(message) => 
        (Message, StatusCode) = (message, (int)statusCode);
}
