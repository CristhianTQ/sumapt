using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Mentoria.Commands.DefinirDisponibilidad;

namespace SUMAPT.Presentation.Controllers.v1.Mentoria;

/// <summary>
/// Controlador para la gestión de horarios y agenda de los mentores.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class DisponibilidadController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa inyectando MediatR.</summary>
    public DisponibilidadController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Añade un bloque de horario semanal a la agenda del mentor.
    /// </summary>
    /// <param name="command">Datos de tiempo y modalidad.</param>
    /// <returns>ID único generado para el bloque de disponibilidad.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Definir([FromBody] DefinirDisponibilidadCommand command)
    {
        var disponibilidadId = await _mediator.Send(command);
        return Created(string.Empty, disponibilidadId);
    }
}