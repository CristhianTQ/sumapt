using System;
using MediatR;

namespace SUMAPT.Application.Analitica.Commands.RegistrarPrediccion;

/// <summary>
/// Comando inmutable para registrar el resultado final de una inferencia de IA.
/// </summary>
public record RegistrarPrediccionCommand(
    Guid InscripcionId,
    decimal ScoreRiesgo,
    string NivelRiesgo,
    string Factores,
    string VersionModelo,
    DateTimeOffset VigenteHasta
) : IRequest<Guid>;