using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearMateria;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador para la administración del pensum y la malla curricular.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MateriaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el controlador inyectando el orquestador CQRS.</summary>
    public MateriaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Añade una nueva Materia (Asignatura) a un Programa Académico.
    /// </summary>
    /// <param name="command">Estructura con dependencias del programa y detalles de la materia.</param>
    /// <returns>ID único generado para la materia.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearMateriaCommand command)
    {
        var materiaId = await _mediator.Send(command);
        return Created(string.Empty, materiaId);
    }
}