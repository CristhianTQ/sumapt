using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearInscripcion;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador principal para gestionar la matrícula de estudiantes.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class InscripcionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el controlador inyectando el orquestador CQRS.</summary>
    public InscripcionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Matricula a un estudiante en un programa de estudios durante un periodo específico.
    /// </summary>
    /// <param name="command">IDs de estudiante, programa y periodo.</param>
    /// <returns>El ID único de la inscripción generada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Matricular([FromBody] CrearInscripcionCommand command)
    {
        var inscripcionId = await _mediator.Send(command);
        return Created(string.Empty, inscripcionId);
    }
}