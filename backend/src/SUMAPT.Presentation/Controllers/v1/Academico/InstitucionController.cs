using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Academico.Commands.CrearInstitucion;

namespace SUMAPT.Presentation.Controllers.v1.Academico;

/// <summary>
/// Controlador para la gestión del Multi-Tenant educativo.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize] // <--- ¡CANDADO MAESTRO! Nadie entra sin un JWT válido.
public class InstitucionController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Inicializa una nueva instancia del controlador inyectando el orquestador CQRS.
    /// </summary>
    /// <param name="mediator">Instancia del despachador de comandos.</param>
    public InstitucionController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra una nueva Institución Educativa en la plataforma.
    /// </summary>
    /// <param name="command">Datos de la institución.</param>
    /// <returns>ID único generado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Documentamos el error 401
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearInstitucionCommand command)
    {
        var institucionId = await _mediator.Send(command);
        
        // Retornamos HTTP 201 Created según las mejores prácticas RESTful
        return Created(string.Empty, institucionId);
    }
}