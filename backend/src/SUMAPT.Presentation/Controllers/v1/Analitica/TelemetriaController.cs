using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Analitica.Commands.RegistrarEvento;

namespace SUMAPT.Presentation.Controllers.v1.Analitica;

/// <summary>
/// Controlador diseñado para la ingesta masiva de interacciones del usuario (Data Pipeline).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class TelemetriaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador de comandos.</summary>
    public TelemetriaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Ingiere un evento atómico desde el frontend para ser procesado por los modelos analíticos.
    /// </summary>
    /// <param name="command">Carga útil del evento estructurado.</param>
    /// <returns>El ID (long) secuencial asignado en base de datos.</returns>
    [HttpPost("ingesta")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarEventoCommand command)
    {
        var eventoId = await _mediator.Send(command);
        return Created(string.Empty, eventoId);
    }
}