using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Comunicacion.Commands.CrearNotificacion;

namespace SUMAPT.Presentation.Controllers.v1.Comunicacion;

/// <summary>
/// Controlador para la gestión y emisión de alertas del sistema.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class NotificacionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Inicializa el controlador inyectando el orquestador MediatR.
    /// </summary>
    /// <param name="mediator">Instancia del despachador de comandos.</param>
    public NotificacionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Emite una nueva notificación a un usuario específico.
    /// </summary>
    /// <param name="command">Estructura del mensaje y destinatario.</param>
    /// <returns>El ID único de la notificación generada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Enviar([FromBody] CrearNotificacionCommand command)
    {
        var notificacionId = await _mediator.Send(command);
        return Created(string.Empty, notificacionId);
    }
}