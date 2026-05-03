using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearPrograma;

/// <summary>
/// Manejador principal para la creación de Programas con validación relacional cruzada.
/// </summary>
public class CrearProgramaHandler : IRequestHandler<CrearProgramaCommand, Guid>
{
    private readonly IRepository<Programa> _programaRepository;
    private readonly IRepository<Institucion> _institucionRepository;
    private readonly IRepository<ModeloAcademico> _modeloRepository;

    /// <summary>
    /// Inyección de múltiples repositorios para comprobaciones de integridad.
    /// </summary>
    public CrearProgramaHandler(
        IRepository<Programa> programaRepository, 
        IRepository<Institucion> institucionRepository,
        IRepository<ModeloAcademico> modeloRepository)
    {
        _programaRepository = programaRepository;
        _institucionRepository = institucionRepository;
        _modeloRepository = modeloRepository;
    }

    public async Task<Guid> Handle(CrearProgramaCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que la Institución exista
        var institucion = await _institucionRepository.GetByIdAsync(request.InstitucionId);
        if (institucion == null)
            throw new Exception("La institución especificada no existe.");

        // 2. Verificamos que el Modelo Académico exista
        var modelo = await _modeloRepository.GetByIdAsync(request.ModeloId);
        if (modelo == null)
            throw new Exception("El modelo académico especificado no existe.");

        // 3. Verificamos que el Modelo pertenezca a la MISMA institución (Seguridad Multitenant)
        if (modelo.InstitucionId != request.InstitucionId)
            throw new Exception("Operación rechazada: El modelo académico no pertenece a la institución especificada.");

        // 4. Instanciación y Persistencia
        var nuevoPrograma = new Programa(
            request.InstitucionId,
            request.ModeloId,
            request.Nombre,
            request.Codigo,
            request.DuracionPeriodos
        );

        await _programaRepository.AddAsync(nuevoPrograma);
        await _programaRepository.SaveChangesAsync();

        return nuevoPrograma.Id;
    }
}