using System;
using MediatR;

namespace SUMAPT.Application.Mentoria.Commands.ReservarCita;

/// <summary>
/// Comando inmutable para solicitar la reserva de un bloque de tiempo con un mentor.
/// </summary>
public record ReservarCitaCommand(
    Guid MentorId,
    Guid EstudianteId,
    Guid? InscripcionId,
    DateTimeOffset FechaHoraIni,
    DateTimeOffset FechaHoraFin,
    string Modalidad,
    string? EnlaceVirtual
) : IRequest<Guid>;