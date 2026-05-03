using System;
using MediatR;

namespace SUMAPT.Application.Comunicacion.Commands.CrearNotificacion;

/// <summary>
/// Comando inmutable para emitir una nueva alerta en el sistema.
/// </summary>
public record CrearNotificacionCommand(
    Guid DestinatarioId,
    string Tipo,
    string Titulo,
    string Cuerpo,
    string? DatosExtra
) : IRequest<Guid>;