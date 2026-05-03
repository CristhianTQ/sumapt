using System;
using MediatR;

namespace SUMAPT.Application.Analitica.Commands.RegistrarEvento;

/// <summary>
/// Comando inmutable de fuego rápido para registrar telemetría.
/// Devuelve un Int64 (long) acorde al BIGSERIAL de la base de datos.
/// </summary>
public record RegistrarEventoCommand(
    Guid UsuarioId,
    Guid? SesionId,
    string TipoEvento,
    string? EntidadTipo,
    Guid? EntidadId,
    string? Metadata
) : IRequest<long>;