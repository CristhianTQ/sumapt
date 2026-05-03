using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Mentoria;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Mentoria.Commands.RegistrarActaSesion;

/// <summary>
/// Orquesta la validación cruzada garantizando la regla 1:1 entre Cita y Acta.
/// </summary>
public class RegistrarActaSesionHandler : IRequestHandler<RegistrarActaSesionCommand, Guid>
{
    private readonly IRepository<ActaSesion> _actaRepository;
    private readonly IRepository<Cita> _citaRepository;

    /// <summary>Inyección cruzada para validar integridad.</summary>
    public RegistrarActaSesionHandler(IRepository<ActaSesion> actaRepository, IRepository<Cita> citaRepository)
    {
        _actaRepository = actaRepository;
        _citaRepository = citaRepository;
    }

    public async Task<Guid> Handle(RegistrarActaSesionCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que la Cita base exista
        var cita = await _citaRepository.GetByIdAsync(request.CitaId);
        if (cita == null)
            throw new Exception("La cita especificada no existe.");

        // 2. Verificación de regla 1:1 (UNIQUE en BD)
        // Una cita solo puede tener un acta. Si ya existe, rechazamos la operación.
        var actasExistentes = await _actaRepository.FindAsync(a => a.CitaId == request.CitaId);
        if (actasExistentes.Any())
            throw new Exception("Esta cita ya cuenta con un acta registrada. No se puede duplicar.");

        // 3. TODO a futuro: Cambiar el estado de la Cita de PENDIENTE a REALIZADA.
        // Por ahora nos centramos en persistir el acta.

        // 4. Instanciación y persistencia
        var nuevaActa = new ActaSesion(
            request.CitaId,
            request.TemasTratados,
            request.Compromisos,
            request.Observaciones,
            request.NivelRiesgoPercibido
        );

        await _actaRepository.AddAsync(nuevaActa);
        await _actaRepository.SaveChangesAsync();

        return nuevaActa.Id;
    }
}