using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // <-- 1. ASEGURA ESTE USING
using SUMAPT.Application.Auth.Commands.SincronizarUsuarioKeycloak;
using SUMAPT.Application.Auth.Queries.GetUsuarioPorKeycloakId;
using SUMAPT.Application.Auth.DTOs;

namespace SUMAPT.Presentation.Controllers.v1.Auth;

/// <summary>
/// Controlador principal para la gestión de identidad y accesos.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize] // <-- 2. ¡EL CANDADO MAESTRO! Solo deja pasar peticiones con Token JWT
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("sincronizar")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Sincronizar([FromBody] SincronizarUsuarioKeycloakCommand command)
    {
        var localUserId = await _mediator.Send(command);
        return Ok(localUserId);
    }

    /// <summary>
    /// Obtiene el perfil público de un usuario utilizando su ID de Keycloak.
    /// </summary>
    /// <param name="keycloakId">El UUID emitido por el proveedor de identidad.</param>
    /// <returns>El DTO con los datos del usuario o 404 si no existe.</returns>
    [HttpGet("keycloak/{keycloakId}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByKeycloakId(Guid keycloakId)
    {
        var query = new GetUsuarioPorKeycloakIdQuery(keycloakId);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "El usuario no existe en la base de datos local." });

        return Ok(result);
    }
}