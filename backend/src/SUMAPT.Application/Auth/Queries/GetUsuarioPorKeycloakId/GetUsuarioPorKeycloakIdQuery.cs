using System;
using MediatR;
using SUMAPT.Application.Auth.DTOs;

namespace SUMAPT.Application.Auth.Queries.GetUsuarioPorKeycloakId;

/// <summary>
/// Consulta de solo lectura (Query) para recuperar el perfil público de un usuario
/// basándose en su identificador único del proveedor de identidad.
/// Retorna un DTO, no una entidad de dominio.
/// </summary>
public record GetUsuarioPorKeycloakIdQuery(Guid KeycloakId) : IRequest<UsuarioDto?>;