using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.RegistrarNota;

/// <summary>
/// Orquesta la inserción del historial académico verificando integridad relacional.
/// </summary>
public class RegistrarNotaHandler : IRequestHandler<RegistrarNotaCommand, Guid>
{
    private readonly IRepository<HistorialNota> _historialRepository;
    private readonly IRepository<Inscripcion> _inscripcionRepository;
    private readonly IRepository<Materia> _materiaRepository;

    public RegistrarNotaHandler(
        IRepository<HistorialNota> historialRepository,
        IRepository<Inscripcion> inscripcionRepository,
        IRepository<Materia> materiaRepository)
    {
        _historialRepository = historialRepository;
        _inscripcionRepository = inscripcionRepository;
        _materiaRepository = materiaRepository;
    }

    public async Task<Guid> Handle(RegistrarNotaCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que la inscripción base exista
        var inscripcion = await _inscripcionRepository.GetByIdAsync(request.InscripcionId);
        if (inscripcion == null) throw new Exception("La inscripción especificada no existe.");

        // 2. Verificamos que la materia exista
        var materia = await _materiaRepository.GetByIdAsync(request.MateriaId);
        if (materia == null) throw new Exception("La materia especificada no existe.");

        // 3. Verificamos que no exista ya una nota finalizada para este alumno en este mismo periodo y materia
        var notasExistentes = await _historialRepository.FindAsync(h => 
            h.InscripcionId == request.InscripcionId && 
            h.MateriaId == request.MateriaId && 
            h.PeriodoId == request.PeriodoId);

        if (notasExistentes.Any())
            throw new Exception("Ya existe un registro de historial para esta materia en el periodo actual.");

        // 4. Instanciación (La entidad calcula si es APROBADO o REPROBADO por sí sola)
        var nuevaNota = new HistorialNota(
            request.InscripcionId,
            request.MateriaId,
            request.PeriodoId,
            request.NotaFinal,
            request.Intentos,
            request.RegistradoPorId
        );

        await _historialRepository.AddAsync(nuevaNota);
        await _historialRepository.SaveChangesAsync();

        return nuevaNota.Id;
    }
}