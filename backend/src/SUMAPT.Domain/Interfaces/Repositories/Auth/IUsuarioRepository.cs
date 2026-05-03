using System;
using System.Threading.Tasks;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Interfaces.Repositories.Auth;

/// <summary>
/// Contrato específico para la entidad Usuario.
/// Extiende el repositorio genérico con búsquedas exclusivas de seguridad.
/// </summary>
public interface IUsuarioRepository : IRepository<Usuario>
{
    /// <summary>Busca un usuario por su correo electrónico único.</summary>
    Task<Usuario?> GetByEmailAsync(string email);
    
    /// <summary>Busca un usuario utilizando el ID emitido por Keycloak.</summary>
    Task<Usuario?> GetByKeycloakIdAsync(Guid keycloakId);
}