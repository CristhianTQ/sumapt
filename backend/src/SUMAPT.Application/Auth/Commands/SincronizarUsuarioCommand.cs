using MediatR;
using Microsoft.EntityFrameworkCore;
using SUMAPT.Domain.Entities;
using SUMAPT.Infrastructure.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SUMAPT.Application.Features.Auth.Commands
{
    public class SincronizarUsuarioCommand : IRequest<Guid>
    {
        public string KeycloakId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string ZonaHoraria { get; set; } = "America/La_Paz";
    }

    public class SincronizarUsuarioCommandHandler : IRequestHandler<SincronizarUsuarioCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public SincronizarUsuarioCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(SincronizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos si el usuario ya existe por su ID de Keycloak
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.KeycloakId == request.KeycloakId, cancellationToken);

            if (usuarioExistente != null)
            {
                // Si existe, simplemente devolvemos su ID interno. No creamos duplicados.
                return usuarioExistente.Id;
            }

            // 2. Si no existe, creamos la nueva entidad
            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                KeycloakId = request.KeycloakId,
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                ZonaHoraria = request.ZonaHoraria,
                FechaRegistro = DateTime.UtcNow
            };

            // 3. Guardamos en PostgreSQL
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync(cancellationToken);

            return nuevoUsuario.Id;
        }
    }
}