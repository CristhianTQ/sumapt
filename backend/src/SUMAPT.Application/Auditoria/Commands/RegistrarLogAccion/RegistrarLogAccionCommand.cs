using System;
using MediatR;

namespace SUMAPT.Application.Auditoria.Commands.RegistrarLogAccion;

/// <summary>
/// Comando inmutable para solicitar el registro asíncrono de una pista de auditoría.
/// </summary>
public record RegistrarLogAccionCommand(
    Guid? UsuarioId,
    string? RolActivo,
    string Accion,
    string EntidadTipo,
    string? EntidadId,
    string? DatosAntes,
    string? DatosDespues,
    string? IpOrigen,
    string? UserAgent,
    string Resultado,
    string? DetalleError
) : IRequest<long>;