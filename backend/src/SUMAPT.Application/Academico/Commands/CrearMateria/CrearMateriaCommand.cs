using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearMateria;

/// <summary>
/// Comando inmutable para solicitar el registro de una nueva Materia en el pensum.
/// </summary>
public record CrearMateriaCommand(
    Guid ProgramaId,
    string Nombre,
    string Codigo,
    short Creditos,
    short? PeriodoSugerido
) : IRequest<Guid>;