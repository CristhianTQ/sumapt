using System;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Domain.Entities.Mentoria;

/// <summary>
/// Representa una reserva o sesión programada entre un estudiante y un mentor.
/// </summary>
public class Cita
{
    /// <summary>Identificador único de la cita.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID del perfil del mentor que impartirá la sesión.</summary>
    public Guid MentorId { get; private set; }
    
    /// <summary>ID del usuario (estudiante) que reserva la sesión.</summary>
    public Guid EstudianteId { get; private set; }
    
    /// <summary>Opcional: ID de la inscripción académica si la cita es específica para una materia/carrera.</summary>
    public Guid? InscripcionId { get; private set; }
    
    /// <summary>Fecha y hora de inicio de la sesión.</summary>
    public DateTimeOffset FechaHoraIni { get; private set; }
    
    /// <summary>Fecha y hora de finalización de la sesión.</summary>
    public DateTimeOffset FechaHoraFin { get; private set; }
    
    /// <summary>Modalidad de la reunión (ej. VIRTUAL, PRESENCIAL).</summary>
    public string Modalidad { get; private set; } = string.Empty;
    
    /// <summary>URL para la reunión en caso de ser virtual (Zoom, Meet, Teams).</summary>
    public string? EnlaceVirtual { get; private set; }
    
    /// <summary>Estado de la reserva (PENDIENTE, CONFIRMADA, REALIZADA, CANCELADA).</summary>
    public string Estado { get; private set; } = "PENDIENTE";
    
    /// <summary>ID del usuario que canceló la cita, si aplica.</summary>
    public Guid? CanceladoPor { get; private set; }
    
    /// <summary>Razón por la cual se canceló la sesión.</summary>
    public string? MotivoCancel { get; private set; }
    
    /// <summary>Sello de tiempo de la creación de la reserva.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia al perfil del mentor asignado a la cita.</summary>
    public PerfilMentor? Mentor { get; private set; }
    
    /// <summary>Referencia al estudiante que solicitó la cita.</summary>
    public Usuario? Estudiante { get; private set; }
    
    /// <summary>Referencia a la inscripción académica (si aplica).</summary>
    public Inscripcion? Inscripcion { get; private set; }
    
    /// <summary>Referencia al usuario que canceló la cita (si fue cancelada).</summary>
    public Usuario? Cancelador { get; private set; }

    /// <summary>Constructor requerido por EF Core.</summary>
    protected Cita() { }

    /// <summary>
    /// Construye una nueva cita aplicando las validaciones de tiempo inquebrantables.
    /// </summary>
    public Cita(Guid mentorId, Guid estudianteId, Guid? inscripcionId, DateTimeOffset fechaHoraIni, DateTimeOffset fechaHoraFin, string modalidad, string? enlaceVirtual)
    {
        if (fechaHoraFin <= fechaHoraIni)
            throw new ArgumentException("La fecha y hora de finalización debe ser estrictamente posterior al inicio.");

        Id = Guid.NewGuid();
        MentorId = mentorId;
        EstudianteId = estudianteId;
        InscripcionId = inscripcionId;
        FechaHoraIni = fechaHoraIni;
        FechaHoraFin = fechaHoraFin;
        Modalidad = string.IsNullOrWhiteSpace(modalidad) ? "VIRTUAL" : modalidad.ToUpperInvariant();
        EnlaceVirtual = enlaceVirtual;
        Estado = "PENDIENTE";
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Cambia el estado de la cita a CANCELADA y registra la justificación.
    /// </summary>
    public void Cancelar(Guid canceladoPorUsuarioId, string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("Debe proporcionar un motivo válido para la cancelación.");

        Estado = "CANCELADA";
        CanceladoPor = canceladoPorUsuarioId;
        MotivoCancel = motivo;
    }
}