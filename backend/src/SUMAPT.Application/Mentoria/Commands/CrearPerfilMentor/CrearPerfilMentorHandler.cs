using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Mentoria;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Mentoria.Commands.CrearPerfilMentor;

/// <summary>
/// Orquesta la lógica de negocio asegurando que un usuario solo pueda tener un perfil de mentor.
/// </summary>
public class CrearPerfilMentorHandler : IRequestHandler<CrearPerfilMentorCommand, Guid>
{
    private readonly IRepository<PerfilMentor> _perfilRepository;
    private readonly IRepository<Usuario> _usuarioRepository;

    /// <summary>Inyecta dependencias hacia Mentoría y Auth.</summary>
    public CrearPerfilMentorHandler(IRepository<PerfilMentor> perfilRepository, IRepository<Usuario> usuarioRepository)
    {
        _perfilRepository = perfilRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(CrearPerfilMentorCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificación de existencia del usuario base
        var usuario = await _usuarioRepository.GetByIdAsync(request.UsuarioId);
        if (usuario == null) 
            throw new Exception("El usuario especificado no existe en el sistema.");

        // 2. Verificación de unicidad 1:1
        var perfilesExistentes = await _perfilRepository.FindAsync(p => p.UsuarioId == request.UsuarioId);
        if (perfilesExistentes.Any()) 
            throw new Exception("Este usuario ya cuenta con un perfil de mentor registrado.");

        // 3. Creación y persistencia
        var nuevoPerfil = new PerfilMentor(
            request.UsuarioId,
            request.Biografia,
            request.Especialidades,
            request.MaxEstudiantes
        );

        await _perfilRepository.AddAsync(nuevoPerfil);
        await _perfilRepository.SaveChangesAsync();

        return nuevoPerfil.Id;
    }
}