using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Academico.Commands.CrearInstitucion;

/// <summary>
/// Lógica de negocio para la creación de una Institución.
/// Utiliza el repositorio genérico inyectado por la capa de Infraestructura.
/// </summary>
public class CrearInstitucionHandler : IRequestHandler<CrearInstitucionCommand, Guid>
{
    private readonly IRepository<Institucion> _institucionRepository;

    public CrearInstitucionHandler(IRepository<Institucion> institucionRepository)
    {
        _institucionRepository = institucionRepository;
    }

    public async Task<Guid> Handle(CrearInstitucionCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos la regla de negocio: El NombreCorto debe ser absolutamente único
        var acronimoNormalizado = request.NombreCorto.ToUpperInvariant();
        var institucionesExistentes = await _institucionRepository.FindAsync(i => i.NombreCorto == acronimoNormalizado);

        if (institucionesExistentes.Any())
        {
            // TODO: Cambiaremos esto por un Custom DomainException en el futuro para devolver HTTP 400
            throw new Exception($"Ya existe una institución registrada con el acrónimo '{acronimoNormalizado}'.");
        }

        // 2. Instanciamos la entidad pura de Dominio
        var nuevaInstitucion = new Institucion(
            request.Nombre,
            request.NombreCorto,
            request.LogoUrl,
            request.DominioEmail,
            request.Pais,
            request.ZonaHoraria
        );

        // 3. Persistimos en PostgreSQL
        await _institucionRepository.AddAsync(nuevaInstitucion);
        await _institucionRepository.SaveChangesAsync();

        return nuevaInstitucion.Id;
    }
}