using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearPeriodo;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador para la gestión de ciclos y periodos académicos.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class PeriodoController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inyección de dependencias del orquestador.</summary>
    public PeriodoController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo Periodo Académico vinculado a una Institución.
    /// </summary>
    /// <param name="command">Estructura con el ID de la institución, nombre y fechas.</param>
    /// <returns>ID único generado para el periodo.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearPeriodoCommand command)
    {
        var periodoId = await _mediator.Send(command);
        return Created(string.Empty, periodoId);
    }
}