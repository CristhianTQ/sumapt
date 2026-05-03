using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearPeriodo;

/// <summary>
/// Manejador que orquesta la validación cruzada y la inserción del Periodo Académico.
/// </summary>
public class CrearPeriodoHandler : IRequestHandler<CrearPeriodoCommand, Guid>
{
    private readonly IRepository<Periodo> _periodoRepository;
    private readonly IRepository<Institucion> _institucionRepository;

    /// <summary>Inyectamos múltiples repositorios genéricos.</summary>
    public CrearPeriodoHandler(IRepository<Periodo> periodoRepository, IRepository<Institucion> institucionRepository)
    {
        _periodoRepository = periodoRepository;
        _institucionRepository = institucionRepository;
    }

    public async Task<Guid> Handle(CrearPeriodoCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificación de Integridad: ¿Existe la institución matriz?
        var institucion = await _institucionRepository.GetByIdAsync(request.InstitucionId);
        if (institucion == null)
        {
            throw new Exception("La institución especificada no existe o fue eliminada.");
        }

        // 2. Verificación de Duplicidad: Evitar dos "Semestre I" en la misma universidad
        var periodosExistentes = await _periodoRepository.FindAsync(p => 
            p.InstitucionId == request.InstitucionId && 
            p.Nombre.ToLower() == request.Nombre.ToLower());

        if (periodosExistentes.Any())
        {
            throw new Exception($"El periodo '{request.Nombre}' ya se encuentra registrado en esta institución.");
        }

        // 3. Creación y Persistencia
        var nuevoPeriodo = new Periodo(
            request.InstitucionId, 
            request.Nombre, 
            request.FechaInicio, 
            request.FechaFin
        );

        await _periodoRepository.AddAsync(nuevoPeriodo);
        await _periodoRepository.SaveChangesAsync();

        return nuevoPeriodo.Id;
    }
}