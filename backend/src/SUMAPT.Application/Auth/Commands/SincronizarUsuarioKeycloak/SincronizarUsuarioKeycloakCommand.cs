using System;
using MediatR;

namespace SUMAPT.Application.Auth.Commands.SincronizarUsuarioKeycloak;

/// <summary>
/// Representa la intención de registrar o actualizar un usuario proveniente de Keycloak en la base de datos local.
/// Utilizamos un 'record' de C# para garantizar que estos datos sean inmutables (no pueden modificarse en tránsito).
/// La interfaz IRequest<Guid> indica que esta operación devolverá el ID local del usuario (UUID).
/// </summary>
public record SincronizarUsuarioKeycloakCommand(
    Guid KeycloakId,
    string Email,
    string Nombre,
    string Apellido,
    string? Telefono,
    string? ZonaHoraria,
    string? Idioma
) : IRequest<Guid>;