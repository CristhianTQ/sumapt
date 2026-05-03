using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.RegistrarNota;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador responsable del registro y modificación de calificaciones (Kardex).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class HistorialNotaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Inicializa una nueva instancia del controlador inyectando el orquestador CQRS.
    /// </summary>
    /// <param name="mediator">Instancia de MediatR para procesar los comandos de notas.</param>
    public HistorialNotaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra el ingreso de una calificación a la malla curricular del estudiante.
    /// </summary>
    /// <param name="command">Datos de la nota y sus IDs foráneos.</param>
    /// <returns>ID único del registro en el historial.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarNotaCommand command)
    {
        var historialId = await _mediator.Send(command);
        return Created(string.Empty, historialId);
    }
}