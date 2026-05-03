using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Auditoria.Commands.RegistrarLogAccion;

namespace SUMAPT.Presentation.Controllers.v1.Auditoria;

/// <summary>
/// Controlador destinado a la ingesta de registros inmutables de auditoría.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class LogAccionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador CQRS.</summary>
    public LogAccionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra un nuevo evento inmutable en el libro mayor del sistema.
    /// </summary>
    /// <param name="command">Datos estructurados de la acción y la entidad afectada.</param>
    /// <returns>ID secuencial generado en base de datos.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarLogAccionCommand command)
    {
        var logId = await _mediator.Send(command);
        return Created(string.Empty, logId);
    }
}