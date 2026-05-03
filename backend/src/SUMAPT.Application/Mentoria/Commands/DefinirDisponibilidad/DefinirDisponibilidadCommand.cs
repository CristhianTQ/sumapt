using System;
using MediatR;

namespace SUMAPT.Application.Mentoria.Commands.DefinirDisponibilidad;

/// <summary>
/// Comando inmutable para registrar un nuevo bloque de horario de un mentor.
/// </summary>
public record DefinirDisponibilidadCommand(
    Guid MentorId,
    short DiaSemana,
    TimeOnly HoraInicio,
    TimeOnly HoraFin,
    string Modalidad
) : IRequest<Guid>;