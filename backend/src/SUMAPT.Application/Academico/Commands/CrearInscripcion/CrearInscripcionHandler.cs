using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearInscripcion;

/// <summary>
/// Orquesta la matrícula del estudiante aplicando validación cruzada profunda.
/// </summary>
public class CrearInscripcionHandler : IRequestHandler<CrearInscripcionCommand, Guid>
{
    private readonly IRepository<Inscripcion> _inscripcionRepository;
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly IRepository<Programa> _programaRepository;
    private readonly IRepository<Periodo> _periodoRepository;

    /// <summary>
    /// Inyección masiva de repositorios para lectura e integridad relacional.
    /// </summary>
    public CrearInscripcionHandler(
        IRepository<Inscripcion> inscripcionRepository,
        IRepository<Usuario> usuarioRepository,
        IRepository<Programa> programaRepository,
        IRepository<Periodo> periodoRepository)
    {
        _inscripcionRepository = inscripcionRepository;
        _usuarioRepository = usuarioRepository;
        _programaRepository = programaRepository;
        _periodoRepository = periodoRepository;
    }

    public async Task<Guid> Handle(CrearInscripcionCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificación: ¿El Estudiante existe en la tabla Auth?
        var estudiante = await _usuarioRepository.GetByIdAsync(request.EstudianteId);
        if (estudiante == null) throw new Exception("El estudiante especificado no existe.");

        // 2. Verificación: ¿El Programa existe?
        var programa = await _programaRepository.GetByIdAsync(request.ProgramaId);
        if (programa == null) throw new Exception("El programa especificado no existe.");

        // 3. Verificación: ¿El Periodo existe?
        var periodo = await _periodoRepository.GetByIdAsync(request.PeriodoId);
        if (periodo == null) throw new Exception("El periodo académico especificado no existe.");

        // 4. Integridad de Negocio: Prevención de doble inscripción
        var inscripcionDuplicada = await _inscripcionRepository.FindAsync(i => 
            i.EstudianteId == request.EstudianteId && 
            i.ProgramaId == request.ProgramaId && 
            i.PeriodoId == request.PeriodoId);

        if (inscripcionDuplicada.Any())
            throw new Exception("El estudiante ya se encuentra matriculado en este programa para el periodo seleccionado.");

        // 5. Instanciación y Persistencia
        var nuevaInscripcion = new Inscripcion(
            request.EstudianteId,
            request.ProgramaId,
            request.PeriodoId
        );

        await _inscripcionRepository.AddAsync(nuevaInscripcion);
        await _inscripcionRepository.SaveChangesAsync();

        return nuevaInscripcion.Id;
    }
}