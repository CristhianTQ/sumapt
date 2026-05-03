using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Analitica.Commands.RegistrarPrediccion;

namespace SUMAPT.Presentation.Controllers.v1.Analitica;

/// <summary>
/// Controlador destinado al servicio de Inteligencia Artificial para volcar sus inferencias.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class PrediccionRiesgoController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador de comandos.</summary>
    public PrediccionRiesgoController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Guarda el resultado del motor predictivo para un estudiante.
    /// </summary>
    /// <param name="command">Cálculo de riesgo, versión del algoritmo y explicabilidad.</param>
    /// <returns>ID único generado para el registro predictivo.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarPrediccionCommand command)
    {
        var prediccionId = await _mediator.Send(command);
        return Created(string.Empty, prediccionId);
    }
}