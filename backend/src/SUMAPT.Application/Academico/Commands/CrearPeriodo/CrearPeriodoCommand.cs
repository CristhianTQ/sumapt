using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearPeriodo;

/// <summary>
/// Comando inmutable para solicitar la creación de un nuevo Periodo Académico.
/// </summary>
public record CrearPeriodoCommand(
    Guid InstitucionId,
    string Nombre,
    DateOnly FechaInicio,
    DateOnly FechaFin
) : IRequest<Guid>;