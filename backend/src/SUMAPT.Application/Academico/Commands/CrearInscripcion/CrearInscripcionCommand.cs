using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearInscripcion;

/// <summary>
/// Comando inmutable para solicitar la matriculación de un estudiante.
/// </summary>
public record CrearInscripcionCommand(
    Guid EstudianteId,
    Guid ProgramaId,
    Guid PeriodoId
) : IRequest<Guid>;