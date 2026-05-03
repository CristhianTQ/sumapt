using System;
using MediatR;

namespace SUMAPT.Application.Mentoria.Commands.RegistrarActaSesion;

/// <summary>
/// Comando inmutable para registrar el acta y finalizar formalmente una sesión de mentoría.
/// </summary>
public record RegistrarActaSesionCommand(
    Guid CitaId,
    string TemasTratados,
    string? Compromisos,
    string? Observaciones,
    string? NivelRiesgoPercibido
) : IRequest<Guid>;