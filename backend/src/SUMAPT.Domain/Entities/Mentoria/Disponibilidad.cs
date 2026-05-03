using System;

namespace SUMAPT.Domain.Entities.Mentoria;

/// <summary>
/// Representa un bloque de horario recurrente en el que un mentor ofrece asesoría.
/// </summary>
public class Disponibilidad
{
    /// <summary>Identificador único del bloque de disponibilidad.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID del perfil del mentor dueño de este horario.</summary>
    public Guid MentorId { get; private set; }
    
    /// <summary>Día de la semana (0 = Domingo, 1 = Lunes, ..., 6 = Sábado).</summary>
    public short DiaSemana { get; private set; }
    
    /// <summary>Hora exacta de inicio del bloque.</summary>
    public TimeOnly HoraInicio { get; private set; }
    
    /// <summary>Hora exacta de finalización del bloque.</summary>
    public TimeOnly HoraFin { get; private set; }
    
    /// <summary>Modalidad de la sesión (ej. VIRTUAL, PRESENCIAL).</summary>
    public string Modalidad { get; private set; } = "VIRTUAL";
    
    /// <summary>Indica si este bloque de horario está actualmente activo para reservas.</summary>
    public bool Activa { get; private set; } = true;

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia el perfil del mentor.</summary>
    public PerfilMentor? Mentor { get; private set; }

    /// <summary>Constructor requerido por Entity Framework Core.</summary>
    protected Disponibilidad() { }

    /// <summary>
    /// Construye un bloque de disponibilidad validando la coherencia del tiempo.
    /// </summary>
    public Disponibilidad(Guid mentorId, short diaSemana, TimeOnly horaInicio, TimeOnly horaFin, string modalidad)
    {
        if (diaSemana < 0 || diaSemana > 6)
            throw new ArgumentException("El día de la semana debe estar entre 0 (Domingo) y 6 (Sábado).");

        if (horaFin <= horaInicio)
            throw new ArgumentException("La hora de finalización debe ser estrictamente posterior a la hora de inicio.");

        Id = Guid.NewGuid();
        MentorId = mentorId;
        DiaSemana = diaSemana;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        Modalidad = string.IsNullOrWhiteSpace(modalidad) ? "VIRTUAL" : modalidad.ToUpperInvariant();
    }

    /// <summary>Desactiva este bloque horario para evitar nuevas reservas.</summary>
    public void Desactivar() => Activa = false;
}