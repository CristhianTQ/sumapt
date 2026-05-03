using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Mentoria.Commands.RegistrarActaSesion;

namespace SUMAPT.Presentation.Controllers.v1.Mentoria;

/// <summary>
/// Controlador para la gestión de las bitácoras y resultados de las sesiones de mentoría.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ActaSesionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el controlador inyectando MediatR.</summary>
    public ActaSesionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra el acta final de una sesión de mentoría.
    /// </summary>
    /// <param name="command">Estructura con el detalle de la sesión y percepciones del mentor.</param>
    /// <returns>El ID único del acta generada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarActaSesionCommand command)
    {
        var actaId = await _mediator.Send(command);
        return Created(string.Empty, actaId);
    }
}