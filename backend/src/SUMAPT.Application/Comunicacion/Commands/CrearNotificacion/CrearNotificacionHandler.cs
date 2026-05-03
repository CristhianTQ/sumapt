using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Comunicacion;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Comunicacion.Commands.CrearNotificacion;

/// <summary>
/// Orquesta la creación de una notificación asegurando que el destinatario sea válido.
/// </summary>
public class CrearNotificacionHandler : IRequestHandler<CrearNotificacionCommand, Guid>
{
    private readonly IRepository<Notificacion> _notificacionRepository;
    private readonly IRepository<Usuario> _usuarioRepository;

    /// <summary>Inyección cruzada de repositorios.</summary>
    public CrearNotificacionHandler(IRepository<Notificacion> notificacionRepository, IRepository<Usuario> usuarioRepository)
    {
        _notificacionRepository = notificacionRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(CrearNotificacionCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que el usuario al que le vamos a notificar exista.
        // No tiene sentido guardar en base de datos una alerta para un usuario fantasma.
        var destinatario = await _usuarioRepository.GetByIdAsync(request.DestinatarioId);
        if (destinatario == null)
            throw new Exception("El usuario destinatario no existe en el sistema.");

        // 2. Instanciación y persistencia
        var nuevaNotificacion = new Notificacion(
            request.DestinatarioId,
            request.Tipo,
            request.Titulo,
            request.Cuerpo,
            request.DatosExtra
        );

        await _notificacionRepository.AddAsync(nuevaNotificacion);
        await _notificacionRepository.SaveChangesAsync();

        return nuevaNotificacion.Id;
    }
}