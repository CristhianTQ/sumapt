using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearPrograma;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador para la gestión de las carreras o programas de estudio.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ProgramaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el controlador inyectando el orquestador MediatR.</summary>
    public ProgramaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo Programa Académico (Carrera).
    /// </summary>
    /// <param name="command">Estructura con dependencias (Institución, Modelo) y detalles de la carrera.</param>
    /// <returns>ID único generado para el programa.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearProgramaCommand command)
    {
        var programaId = await _mediator.Send(command);
        return Created(string.Empty, programaId);
    }
}