using System;
using MediatR;

namespace SUMAPT.Application.Comunicacion.Commands.IniciarConversacion;

/// <summary>
/// Comando para abrir un canal de chat entre dos usuarios.
/// Retorna el ID de la conversación (nueva o existente).
/// </summary>
public record IniciarConversacionCommand(
    Guid IniciadorId,
    Guid ReceptorId
) : IRequest<Guid>;