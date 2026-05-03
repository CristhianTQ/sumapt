using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.RegistrarNota;

/// <summary>
/// Comando inmutable para solicitar el registro de una calificación.
/// </summary>
public record RegistrarNotaCommand(
    Guid InscripcionId,
    Guid MateriaId,
    Guid PeriodoId,
    decimal? NotaFinal,
    short Intentos,
    Guid? RegistradoPorId
) : IRequest<Guid>;