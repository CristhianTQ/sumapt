using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Mentoria;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Mentoria.Commands.DefinirDisponibilidad;

/// <summary>
/// Lógica de negocio para asignar horarios a un mentor existente.
/// </summary>
public class DefinirDisponibilidadHandler : IRequestHandler<DefinirDisponibilidadCommand, Guid>
{
    private readonly IRepository<Disponibilidad> _disponibilidadRepository;
    private readonly IRepository<PerfilMentor> _mentorRepository;

    /// <summary>Inyección cruzada de repositorios.</summary>
    public DefinirDisponibilidadHandler(
        IRepository<Disponibilidad> disponibilidadRepository,
        IRepository<PerfilMentor> mentorRepository)
    {
        _disponibilidadRepository = disponibilidadRepository;
        _mentorRepository = mentorRepository;
    }

    public async Task<Guid> Handle(DefinirDisponibilidadCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar que el mentor realmente exista antes de asignarle horarios
        var mentor = await _mentorRepository.GetByIdAsync(request.MentorId);
        if (mentor == null)
            throw new Exception("El perfil de mentor especificado no existe.");

        // TODO a futuro: Lógica para evitar solapamiento exacto de horarios en el mismo día.
        // Por ahora nos enfocamos en persistir el bloque de disponibilidad puro.

        // 2. Instanciación pura
        var nuevaDisponibilidad = new Disponibilidad(
            request.MentorId,
            request.DiaSemana,
            request.HoraInicio,
            request.HoraFin,
            request.Modalidad
        );

        // 3. Persistencia
        await _disponibilidadRepository.AddAsync(nuevaDisponibilidad);
        await _disponibilidadRepository.SaveChangesAsync();

        return nuevaDisponibilidad.Id;
    }
}