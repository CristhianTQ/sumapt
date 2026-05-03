using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Comunicacion.Commands.EnviarMensaje;

namespace SUMAPT.Presentation.Controllers.v1.Comunicacion;

/// <summary>
/// Controlador responsable de la ingesta de mensajes de chat.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MensajeController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador CQRS.</summary>
    public MensajeController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Envía un nuevo mensaje de texto a una conversación existente.
    /// </summary>
    /// <param name="command">Datos de ruteo y contenido del mensaje.</param>
    /// <returns>El ID único del mensaje insertado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Enviar([FromBody] EnviarMensajeCommand command)
    {
        try
        {
            var mensajeId = await _mediator.Send(command);
            return Created(string.Empty, mensajeId);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}