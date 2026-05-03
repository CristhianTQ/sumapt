using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Interfaces.Repositories.Auth;

namespace SUMAPT.Infrastructure.Persistence.Repositories.Auth;

/// <summary>
/// Implementación física de las operaciones de base de datos exclusivas para Usuarios.
/// </summary>
public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> GetByKeycloakIdAsync(Guid keycloakId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId);
    }
}