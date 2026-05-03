using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearModeloAcademico;

/// <summary>
/// Manejador que orquesta la creación del Modelo Académico.
/// </summary>
public class CrearModeloAcademicoHandler : IRequestHandler<CrearModeloAcademicoCommand, Guid>
{
    private readonly IRepository<ModeloAcademico> _modeloRepository;
    private readonly IRepository<Institucion> _institucionRepository;

    public CrearModeloAcademicoHandler(IRepository<ModeloAcademico> modeloRepository, IRepository<Institucion> institucionRepository)
    {
        _modeloRepository = modeloRepository;
        _institucionRepository = institucionRepository;
    }

    public async Task<Guid> Handle(CrearModeloAcademicoCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificación de Integridad: ¿Existe la institución matriz?
        var institucion = await _institucionRepository.GetByIdAsync(request.InstitucionId);
        if (institucion == null)
        {
            throw new Exception("La institución especificada no existe o fue eliminada.");
        }

        // 2. Creación y Persistencia
        var nuevoModelo = new ModeloAcademico(
            request.InstitucionId, 
            request.Nombre, 
            request.Tipo, 
            request.PeriodosPorAño
        );

        await _modeloRepository.AddAsync(nuevoModelo);
        await _modeloRepository.SaveChangesAsync();

        return nuevoModelo.Id;
    }
}