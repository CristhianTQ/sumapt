using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Comunicacion;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Comunicacion.Commands.EnviarMensaje;

/// <summary>
/// Orquesta el almacenamiento de un mensaje verificando la integridad de los actores.
/// </summary>
public class EnviarMensajeHandler : IRequestHandler<EnviarMensajeCommand, Guid>
{
    private readonly IRepository<Mensaje> _mensajeRepository;
    private readonly IRepository<Conversacion> _conversacionRepository;
    private readonly IRepository<Usuario> _usuarioRepository;

    /// <summary>Inyección cruzada de repositorios para validación estricta.</summary>
    public EnviarMensajeHandler(
        IRepository<Mensaje> mensajeRepository,
        IRepository<Conversacion> conversacionRepository,
        IRepository<Usuario> usuarioRepository)
    {
        _mensajeRepository = mensajeRepository;
        _conversacionRepository = conversacionRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(EnviarMensajeCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que el chat exista
        var conversacion = await _conversacionRepository.GetByIdAsync(request.ConversacionId);
        if (conversacion == null)
            throw new Exception("La conversación especificada no existe.");

        // 2. Verificamos que el remitente exista
        var remitente = await _usuarioRepository.GetByIdAsync(request.RemitenteId);
        if (remitente == null)
            throw new Exception("El usuario remitente no existe en el sistema.");

        // 3. Validación de Negocio Crítica: ¿El remitente es parte de esta conversación?
        // Previene que un usuario inyecte mensajes en un chat privado de otras dos personas.
        if (request.RemitenteId != conversacion.ParticipanteA && request.RemitenteId != conversacion.ParticipanteB)
            throw new UnauthorizedAccessException("El usuario no es participante de esta conversación.");

        // 4. Instanciación y persistencia
        var nuevoMensaje = new Mensaje(
            request.ConversacionId,
            request.RemitenteId,
            request.Contenido
        );

        await _mensajeRepository.AddAsync(nuevoMensaje);
        await _mensajeRepository.SaveChangesAsync();

        return nuevoMensaje.Id;
    }
}