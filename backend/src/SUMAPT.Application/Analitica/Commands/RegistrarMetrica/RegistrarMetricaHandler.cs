using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Entities.Analitica;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Analitica.Commands.RegistrarMetrica;

/// <summary>
/// Lógica de negocio encargada de validar las referencias foráneas antes de guardar el KPI.
/// </summary>
public class RegistrarMetricaHandler : IRequestHandler<RegistrarMetricaCommand, Guid>
{
    private readonly IRepository<MetricaAgregada> _metricaRepository;
    private readonly IRepository<Institucion> _institucionRepository;
    private readonly IRepository<Periodo> _periodoRepository;

    /// <summary>Inyección cruzada de repositorios para validación estructural.</summary>
    public RegistrarMetricaHandler(
        IRepository<MetricaAgregada> metricaRepository,
        IRepository<Institucion> institucionRepository,
        IRepository<Periodo> periodoRepository)
    {
        _metricaRepository = metricaRepository;
        _institucionRepository = institucionRepository;
        _periodoRepository = periodoRepository;
    }

    public async Task<Guid> Handle(RegistrarMetricaCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos la existencia de la institución
        var institucion = await _institucionRepository.GetByIdAsync(request.InstitucionId);
        if (institucion == null)
            throw new Exception("La institución especificada no existe en el sistema.");

        // 2. Si se proporcionó un Periodo, validamos que exista
        if (request.PeriodoId.HasValue)
        {
            var periodo = await _periodoRepository.GetByIdAsync(request.PeriodoId.Value);
            if (periodo == null)
                throw new Exception("El periodo académico especificado no existe.");
        }

        // 3. Instanciación y guardado
        var nuevaMetrica = new MetricaAgregada(
            request.InstitucionId,
            request.PeriodoId,
            request.TipoMetrica,
            request.Valor
        );

        await _metricaRepository.AddAsync(nuevaMetrica);
        await _metricaRepository.SaveChangesAsync();

        return nuevaMetrica.Id;
    }
}