using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Entities.Analitica;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Analitica.Commands.RegistrarPrediccion;

/// <summary>
/// Orquesta el guardado del análisis predictivo validando dependencias cruzadas.
/// </summary>
public class RegistrarPrediccionHandler : IRequestHandler<RegistrarPrediccionCommand, Guid>
{
    private readonly IRepository<PrediccionRiesgo> _prediccionRepository;
    private readonly IRepository<Inscripcion> _inscripcionRepository;

    /// <summary>Inyección de repositorios del módulo Analítico y Académico.</summary>
    public RegistrarPrediccionHandler(
        IRepository<PrediccionRiesgo> prediccionRepository,
        IRepository<Inscripcion> inscripcionRepository)
    {
        _prediccionRepository = prediccionRepository;
        _inscripcionRepository = inscripcionRepository;
    }

    public async Task<Guid> Handle(RegistrarPrediccionCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que la inscripción académica analizada exista realmente.
        var inscripcion = await _inscripcionRepository.GetByIdAsync(request.InscripcionId);
        if (inscripcion == null)
            throw new Exception("La inscripción especificada no existe o fue eliminada.");

        // 2. Instanciación y persistencia
        var nuevaPrediccion = new PrediccionRiesgo(
            request.InscripcionId,
            request.ScoreRiesgo,
            request.NivelRiesgo,
            request.Factores,
            request.VersionModelo,
            request.VigenteHasta
        );

        await _prediccionRepository.AddAsync(nuevaPrediccion);
        await _prediccionRepository.SaveChangesAsync();

        return nuevaPrediccion.Id;
    }
}