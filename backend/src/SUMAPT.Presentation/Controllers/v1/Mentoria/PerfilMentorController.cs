using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Mentoria.Commands.CrearPerfilMentor;

namespace SUMAPT.Presentation.Controllers.v1.Mentoria;

/// <summary>
/// Controlador responsable de gestionar la activación y perfiles de los mentores.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class PerfilMentorController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el controlador con el orquestador MediatR.</summary>
    public PerfilMentorController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Habilita a un usuario existente en la plataforma como Mentor.
    /// </summary>
    /// <param name="command">Estructura con el ID del usuario, su biografía y límite de capacidad.</param>
    /// <returns>ID único generado para el perfil de mentor.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Crear([FromBody] CrearPerfilMentorCommand command)
    {
        var perfilId = await _mediator.Send(command);
        return Created(string.Empty, perfilId);
    }
}