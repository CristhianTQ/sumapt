using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearModeloAcademico;

/// <summary>
/// Comando inmutable para solicitar la creación de un Modelo Académico.
/// </summary>
public record CrearModeloAcademicoCommand(
    Guid InstitucionId,
    string Nombre,
    string Tipo,
    int PeriodosPorAño
) : IRequest<Guid>;