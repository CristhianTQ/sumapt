using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Application.Auth.DTOs;
using SUMAPT.Domain.Interfaces.Repositories.Auth;

namespace SUMAPT.Application.Auth.Queries.GetUsuarioPorKeycloakId;

/// <summary>
/// Manejador de la consulta. Extrae la entidad pura y la mapea a un formato seguro (DTO).
/// </summary>
public class GetUsuarioPorKeycloakIdHandler : IRequestHandler<GetUsuarioPorKeycloakIdQuery, UsuarioDto?>
{
    private readonly IUsuarioRepository _usuarioRepository;

    /// <summary>Inyectamos el repositorio específico de seguridad.</summary>
    public GetUsuarioPorKeycloakIdHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto?> Handle(GetUsuarioPorKeycloakIdQuery request, CancellationToken cancellationToken)
    {
        // 1. Buscamos en la base de datos usando el contrato de infraestructura
        var usuario = await _usuarioRepository.GetByKeycloakIdAsync(request.KeycloakId);

        // 2. Si no existe, devolvemos null (el controlador decidirá qué HTTP Status enviar)
        if (usuario == null) return null;

        // 3. Mapeo Manual (TODO: En el futuro automatizaremos esto con Riok.Mapperly)
        // Convertimos la Entidad de Dominio en un DTO ligero.
        return new UsuarioDto(
            usuario.Id,
            usuario.Email,
            usuario.Nombre,
            usuario.Apellido,
            usuario.AvatarUrl,
            usuario.Idioma,
            usuario.ZonaHoraria
        );
    }
}