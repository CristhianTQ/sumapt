using System;
using MediatR;

namespace SUMAPT.Application.Analitica.Commands.RegistrarMetrica;

/// <summary>
/// Comando inmutable para inyectar un valor estadístico calculado por un proceso asíncrono.
/// </summary>
public record RegistrarMetricaCommand(
    Guid InstitucionId,
    Guid? PeriodoId,
    string TipoMetrica,
    decimal Valor
) : IRequest<Guid>;