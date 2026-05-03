using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearModeloAcademico;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador para la gestión de las plantillas y modelos académicos (Semestral, Modular, etc.).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ModeloAcademicoController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Inicializa una nueva instancia del controlador inyectando el orquestador.
    /// </summary>
    /// <param name="mediator">Instancia de MediatR para el envío de comandos.</param>
    public ModeloAcademicoController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo Modelo Académico vinculado a una Institución.
    /// </summary>
    /// <param name="command">Estructura con el ID de la institución, nombre y configuración anual.</param>
    /// <returns>ID único generado para el modelo.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearModeloAcademicoCommand command)
    {
        var modeloId = await _mediator.Send(command);
        return Created(string.Empty, modeloId);
    }
}