using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Mentoria.Commands.ReservarCita;

namespace SUMAPT.Presentation.Controllers.v1.Mentoria;

/// <summary>
/// Controlador principal para la gestión de reservas de mentoría y citas académicas.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CitaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador CQRS.</summary>
    public CitaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra una nueva reserva de mentoría para un estudiante.
    /// </summary>
    /// <param name="command">Datos de programación de la cita.</param>
    /// <returns>El ID único de la reserva generada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Reservar([FromBody] ReservarCitaCommand command)
    {
        var citaId = await _mediator.Send(command);
        return Created(string.Empty, citaId);
    }
}