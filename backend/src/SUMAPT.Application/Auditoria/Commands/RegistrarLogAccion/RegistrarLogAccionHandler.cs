using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auditoria;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Auditoria.Commands.RegistrarLogAccion;

/// <summary>
/// Handler de inserción rápida para la auditoría. Evita validaciones foráneas complejas 
/// para no ralentizar las transacciones principales del sistema.
/// </summary>
public class RegistrarLogAccionHandler : IRequestHandler<RegistrarLogAccionCommand, long>
{
    private readonly IRepository<LogAccion> _logRepository;

    /// <summary>Inyecta el repositorio puro de auditoría.</summary>
    public RegistrarLogAccionHandler(IRepository<LogAccion> logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<long> Handle(RegistrarLogAccionCommand request, CancellationToken cancellationToken)
    {
        var nuevoLog = new LogAccion(
            request.UsuarioId,
            request.RolActivo,
            request.Accion,
            request.EntidadTipo,
            request.EntidadId,
            request.DatosAntes,
            request.DatosDespues,
            request.IpOrigen,
            request.UserAgent,
            request.Resultado,
            request.DetalleError
        );

        await _logRepository.AddAsync(nuevoLog);
        await _logRepository.SaveChangesAsync();

        return nuevoLog.Id;
    }
}