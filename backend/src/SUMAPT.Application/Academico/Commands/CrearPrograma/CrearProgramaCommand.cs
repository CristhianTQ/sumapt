using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearPrograma;

/// <summary>
/// Comando inmutable para solicitar el registro de un nuevo Programa Académico.
/// </summary>
public record CrearProgramaCommand(
    Guid InstitucionId,
    Guid ModeloId,
    string Nombre,
    string? Codigo,
    int DuracionPeriodos
) : IRequest<Guid>;