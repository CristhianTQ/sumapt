using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearMateria;

/// <summary>
/// Lógica central de negocio para asignar una materia a una malla curricular.
/// </summary>
public class CrearMateriaHandler : IRequestHandler<CrearMateriaCommand, Guid>
{
    private readonly IRepository<Materia> _materiaRepository;
    private readonly IRepository<Programa> _programaRepository;

    public CrearMateriaHandler(IRepository<Materia> materiaRepository, IRepository<Programa> programaRepository)
    {
        _materiaRepository = materiaRepository;
        _programaRepository = programaRepository;
    }

    public async Task<Guid> Handle(CrearMateriaCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que el Programa (carrera) exista
        var programa = await _programaRepository.GetByIdAsync(request.ProgramaId);
        if (programa == null)
            throw new Exception("El programa especificado no existe.");

        // 2. Verificación de Unicidad Combinada: La misma carrera no puede tener dos materias con el mismo código
        var codigoNormalizado = request.Codigo.ToUpperInvariant();
        var materiaDuplicada = await _materiaRepository.FindAsync(m => 
            m.ProgramaId == request.ProgramaId && 
            m.Codigo == codigoNormalizado);

        if (materiaDuplicada.Any())
            throw new Exception($"El programa ya tiene una materia registrada con el código '{codigoNormalizado}'.");

        // 3. Instanciamos y guardamos la entidad
        var nuevaMateria = new Materia(
            request.ProgramaId,
            request.Nombre,
            request.Codigo,
            request.Creditos,
            request.PeriodoSugerido
        );

        await _materiaRepository.AddAsync(nuevaMateria);
        await _materiaRepository.SaveChangesAsync();

        return nuevaMateria.Id;
    }
}