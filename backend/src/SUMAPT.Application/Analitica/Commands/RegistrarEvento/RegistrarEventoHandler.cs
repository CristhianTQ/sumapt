using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Analitica;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Analitica.Commands.RegistrarEvento;

/// <summary>
/// Handler de alta velocidad (Fire and Forget lógico) para ingesta de métricas.
/// Retorna long en respuesta a la secuencia BIGSERIAL.
/// </summary>
public class RegistrarEventoHandler : IRequestHandler<RegistrarEventoCommand, long>
{
    private readonly IRepository<EventoTelemetria> _telemetriaRepository;

    /// <summary>Inyecta únicamente el repositorio de telemetría para máxima eficiencia.</summary>
    public RegistrarEventoHandler(IRepository<EventoTelemetria> telemetriaRepository)
    {
        // En telemetría evitamos validar la existencia física del usuario para no recargar la base de datos en cada clic.
        // Asumimos que el ID proporcionado por el Token JWT en el frontend es correcto.
        _telemetriaRepository = telemetriaRepository;
    }

    public async Task<long> Handle(RegistrarEventoCommand request, CancellationToken cancellationToken)
    {
        var nuevoEvento = new EventoTelemetria(
            request.UsuarioId,
            request.SesionId,
            request.TipoEvento,
            request.EntidadTipo,
            request.EntidadId,
            request.Metadata
        );

        await _telemetriaRepository.AddAsync(nuevoEvento);
        await _telemetriaRepository.SaveChangesAsync();

        // El ORM rellena la propiedad Id con el valor autogenerado por PostgreSQL al insertar.
        return nuevoEvento.Id;
    }
}