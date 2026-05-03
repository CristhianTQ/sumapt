using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Interfaces.Repositories.Auth;

namespace SUMAPT.Application.Auth.Commands.SincronizarUsuarioKeycloak;

/// <summary>
/// Contiene la lógica pura de negocio para la sincronización de usuarios.
/// Desacoplado totalmente de Entity Framework o la Web API.
/// </summary>
public class SincronizarUsuarioKeycloakHandler : IRequestHandler<SincronizarUsuarioKeycloakCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;

    // Inyección de dependencias: Pedimos el contrato (Interface), no la implementación física.
    public SincronizarUsuarioKeycloakHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(SincronizarUsuarioKeycloakCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos si el usuario ya existe usando el método de búsqueda que creamos en el Repositorio
        var usuarioExistente = await _usuarioRepository.GetByKeycloakIdAsync(request.KeycloakId);

        if (usuarioExistente != null)
        {
            // El usuario ya ingresó antes al sistema.
            // TODO: Podríamos actualizar su nombre o email aquí si cambió en Keycloak, 
            // pero por ahora simplemente retornamos su ID local.
            return usuarioExistente.Id;
        }

        // 2. Si no existe, instanciamos la entidad de Dominio pura
        // Asignamos por defecto 'America/La_Paz' para el contexto de Bolivia
        var nuevoUsuario = new Usuario(
            request.KeycloakId,
            request.Email,
            request.Nombre,
            request.Apellido,
            request.Telefono,
            null, // AvatarUrl
            request.ZonaHoraria ?? "America/La_Paz", 
            request.Idioma ?? "es"
        );

        // 3. Lo registramos en el repositorio en memoria
        await _usuarioRepository.AddAsync(nuevoUsuario);

        // 4. Ejecutamos la transacción en la Base de Datos Física (PostgreSQL)
        await _usuarioRepository.SaveChangesAsync();

        return nuevoUsuario.Id;
    }
}