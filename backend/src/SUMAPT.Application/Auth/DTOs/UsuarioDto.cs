using System;

namespace SUMAPT.Application.Auth.DTOs;

/// <summary>
/// Objeto ligero de transferencia de datos.
/// Protege la entidad de dominio y solo expone la información que la UI realmente necesita.
/// </summary>
public record UsuarioDto(
    Guid Id,
    string Email,
    string Nombre,
    string Apellido,
    string? AvatarUrl,
    string Idioma,
    string ZonaHoraria
);