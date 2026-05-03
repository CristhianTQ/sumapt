using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa la matrícula de un estudiante en un programa durante un periodo específico.
/// </summary>
public class Inscripcion
{
    /// <summary>Identificador único de la inscripción.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID del usuario (estudiante) inscrito.</summary>
    public Guid EstudianteId { get; private set; }
    
    /// <summary>ID del programa o carrera al que se inscribe.</summary>
    public Guid ProgramaId { get; private set; }
    
    /// <summary>ID del periodo académico (ej. Semestre I-2026).</summary>
    public Guid PeriodoId { get; private set; }
    
    /// <summary>Estado actual de la inscripción (ej. ACTIVO, SUSPENDIDO, RETIRADO).</summary>
    public string Estado { get; private set; } = "ACTIVO";
    
    /// <summary>Fecha y hora en que se registró la inscripción.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia el Estudiante.</summary>
    public Usuario? Estudiante { get; private set; }
    
    /// <summary>Referencia hacia el Programa.</summary>
    public Programa? Programa { get; private set; }
    
    /// <summary>Referencia hacia el Periodo.</summary>
    public Periodo? Periodo { get; private set; }

    /// <summary>Constructor requerido por EF Core.</summary>
    protected Inscripcion() { }

    /// <summary>
    /// Genera una nueva inscripción en estado ACTIVO por defecto.
    /// </summary>
    public Inscripcion(Guid estudianteId, Guid programaId, Guid periodoId)
    {
        Id = Guid.NewGuid();
        EstudianteId = estudianteId;
        ProgramaId = programaId;
        PeriodoId = periodoId;
        Estado = "ACTIVO";
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Modifica el estado de la matrícula mediante una regla de negocio.
    /// </summary>
    /// <param name="nuevoEstado">El nuevo estado (Ej. RETIRADO).</param>
    public void CambiarEstado(string nuevoEstado)
    {
        if (string.IsNullOrWhiteSpace(nuevoEstado)) 
            throw new ArgumentException("El estado no puede estar vacío.");
            
        Estado = nuevoEstado.ToUpperInvariant();
    }
}