using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Mentoria;
using SUMAPT.Domain.Interfaces.Repositories;

namespace SUMAPT.Application.Mentoria.Commands.ReservarCita;

/// <summary>
/// Orquesta la lógica de negocio para consolidar la reserva de mentoría.
/// </summary>
public class ReservarCitaHandler : IRequestHandler<ReservarCitaCommand, Guid>
{
    private readonly IRepository<Cita> _citaRepository;
    private readonly IRepository<PerfilMentor> _mentorRepository;
    private readonly IRepository<Usuario> _usuarioRepository;

    /// <summary>Inyección cruzada de repositorios para validación transaccional.</summary>
    public ReservarCitaHandler(
        IRepository<Cita> citaRepository,
        IRepository<PerfilMentor> mentorRepository,
        IRepository<Usuario> usuarioRepository)
    {
        _citaRepository = citaRepository;
        _mentorRepository = mentorRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(ReservarCitaCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificamos que el mentor exista y esté activo
        var mentor = await _mentorRepository.GetByIdAsync(request.MentorId);
        if (mentor == null || !mentor.Activo)
            throw new Exception("El mentor especificado no existe o no está disponible actualmente.");

        // 2. Verificamos que el estudiante sea real
        var estudiante = await _usuarioRepository.GetByIdAsync(request.EstudianteId);
        if (estudiante == null)
            throw new Exception("El estudiante especificado no existe en el sistema.");

        // 3. No permitimos que un mentor se reserve una cita a sí mismo
        if (mentor.UsuarioId == request.EstudianteId)
            throw new Exception("Un mentor no puede reservarse una cita a sí mismo.");

        // 4. Instanciación y persistencia
        var nuevaCita = new Cita(
            request.MentorId,
            request.EstudianteId,
            request.InscripcionId,
            request.FechaHoraIni,
            request.FechaHoraFin,
            request.Modalidad,
            request.EnlaceVirtual
        );

        await _citaRepository.AddAsync(nuevaCita);
        await _citaRepository.SaveChangesAsync();

        return nuevaCita.Id;
    }
}