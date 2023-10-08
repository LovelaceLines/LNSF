using LNSF.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
    public AppException Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var serverErrorMessage = "Um erro inesperado acaba de acontecer! Por favor, contate a equipe de desenvolvimento para a análise e correção do erro.";
        var message = (context?.Error as AppException)?.Message ?? serverErrorMessage;
        var statusCode = (context?.Error as AppException)?.StatusCode ?? 500;
        var id = HttpContext.TraceIdentifier;
        
        return new AppException(message, statusCode) { Id = id };
    }
}
