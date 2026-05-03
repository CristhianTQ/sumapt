using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Comunicacion;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Comunicacion.Commands.IniciarConversacion;

/// <summary>
/// Lógica de negocio que busca un chat existente, o lo crea si no existe (Patrón Idempotente).
/// </summary>
public class IniciarConversacionHandler : IRequestHandler<IniciarConversacionCommand, Guid>
{
    private readonly IRepository<Conversacion> _conversacionRepository;
    private readonly IRepository<Usuario> _usuarioRepository;

    public IniciarConversacionHandler(IRepository<Conversacion> conversacionRepository, IRepository<Usuario> usuarioRepository)
    {
        _conversacionRepository = conversacionRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(IniciarConversacionCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar que ambos usuarios existan físicamente en la BD
        var iniciador = await _usuarioRepository.GetByIdAsync(request.IniciadorId);
        var receptor = await _usuarioRepository.GetByIdAsync(request.ReceptorId);

        if (iniciador == null || receptor == null)
            throw new Exception("Uno o ambos usuarios no existen en el sistema.");

        // 2. Aplicar la misma lógica de ordenamiento del Dominio para la búsqueda
        var partA = request.IniciadorId.CompareTo(request.ReceptorId) < 0 ? request.IniciadorId : request.ReceptorId;
        var partB = request.IniciadorId.CompareTo(request.ReceptorId) < 0 ? request.ReceptorId : request.IniciadorId;

        // 3. Buscar si ya existe el chat
        var chatsExistentes = await _conversacionRepository.FindAsync(c => c.ParticipanteA == partA && c.ParticipanteB == partB);
        var chatActual = chatsExistentes.FirstOrDefault();

        // 4. Patrón Idempotente: Si ya existe, devolvemos su ID. Si no, lo creamos.
        if (chatActual != null)
        {
            return chatActual.Id;
        }

        var nuevaConversacion = new Conversacion(request.IniciadorId, request.ReceptorId);
        
        await _conversacionRepository.AddAsync(nuevaConversacion);
        await _conversacionRepository.SaveChangesAsync();

        return nuevaConversacion.Id;
    }
}