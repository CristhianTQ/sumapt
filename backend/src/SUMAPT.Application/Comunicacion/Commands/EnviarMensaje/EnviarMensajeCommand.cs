using System;
using MediatR;

namespace SUMAPT.Application.Comunicacion.Commands.EnviarMensaje;

/// <summary>
/// Comando inmutable para solicitar el envío de un texto dentro de un chat existente.
/// </summary>
public record EnviarMensajeCommand(
    Guid ConversacionId,
    Guid RemitenteId,
    string Contenido
) : IRequest<Guid>;