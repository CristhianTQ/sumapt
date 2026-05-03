using System;
using MediatR;

namespace SUMAPT.Application.Academico.Commands.CrearInstitucion;

/// <summary>
/// Comando inmutable para registrar una nueva Institución en el sistema.
/// </summary>
public record CrearInstitucionCommand(
    string Nombre,
    string NombreCorto,
    string? LogoUrl,
    string? DominioEmail,
    string? Pais,
    string? ZonaHoraria
) : IRequest<Guid>;