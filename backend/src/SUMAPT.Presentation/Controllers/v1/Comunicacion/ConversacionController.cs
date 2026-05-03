using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Comunicacion.Commands.IniciarConversacion;

namespace SUMAPT.Presentation.Controllers.v1.Comunicacion;

/// <summary>
/// Controlador para la gestión de chats privados y mensajería directa.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ConversacionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Inicializa una nueva instancia del controlador inyectando el orquestador CQRS.
    /// </summary>
    /// <param name="mediator">Instancia de MediatR para despachar los comandos.</param>
    public ConversacionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Inicia un nuevo chat entre dos usuarios o devuelve el ID del chat si ya existe.
    /// </summary>
    /// <param name="command">IDs del usuario que inicia y el receptor.</param>
    /// <returns>ID único de la conversación (nueva o existente).</returns>
    [HttpPost("iniciar")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Iniciar([FromBody] IniciarConversacionCommand command)
    {
        var conversacionId = await _mediator.Send(command);
        // Usamos 200 OK en lugar de 201 Created porque la acción es idempotente (podría devolver algo existente)
        return Ok(conversacionId);
    }
}