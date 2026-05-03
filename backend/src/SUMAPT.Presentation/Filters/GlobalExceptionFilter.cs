using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SUMAPT.Presentation.Filters;

/// <summary>
/// Intercepta cualquier excepción no manejada en el sistema y la formatea
/// como una respuesta JSON estándar (ProblemDetails), protegiendo la información sensible.
/// </summary>
public class GlobalExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Método disparado automáticamente por el framework cuando ocurre un error no capturado
    /// durante el procesamiento de una petición HTTP.
    /// </summary>
    /// <param name="context">Contexto actual de la excepción y la petición.</param>
    public void OnException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error Interno del Servidor",
            Detail = context.Exception.Message,
            Instance = context.HttpContext.Request.Path
        };

        // TODO: En el futuro, aquí interceptaremos "DomainExceptions" y "NotFoundExceptions" 
        // para devolver 400 Bad Request o 404 Not Found dinámicamente.

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}