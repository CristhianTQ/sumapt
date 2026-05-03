using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa el registro de calificaciones de un estudiante en una materia específica.
/// Alimenta directamente al modelo de predicción de riesgo.
/// </summary>
public class HistorialNota
{
    /// <summary>Identificador único del registro de nota.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID de la inscripción del estudiante.</summary>
    public Guid InscripcionId { get; private set; }
    
    /// <summary>ID de la materia cursada.</summary>
    public Guid MateriaId { get; private set; }
    
    /// <summary>ID del periodo en el que se cursó.</summary>
    public Guid PeriodoId { get; private set; }
    
    /// <summary>Calificación final obtenida (0 a 100).</summary>
    public decimal? NotaFinal { get; private set; }
    
    /// <summary>Estado académico: EN_CURSO, APROBADO, REPROBADO, ABANDONO.</summary>
    public string EstadoCurso { get; private set; } = "EN_CURSO";
    
    /// <summary>Número de vez que el estudiante cursa esta materia.</summary>
    public short Intentos { get; private set; } = 1;
    
    /// <summary>Fecha exacta del registro de la calificación.</summary>
    public DateTimeOffset RegistradoEn { get; private set; }
    
    /// <summary>ID del usuario (Docente o Admin) que subió la nota.</summary>
    public Guid? RegistradoPor { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia a la inscripción base del estudiante.</summary>
    public Inscripcion? Inscripcion { get; private set; }
    
    /// <summary>Referencia a la materia cursada.</summary>
    public Materia? Materia { get; private set; }
    
    /// <summary>Referencia al periodo académico evaluado.</summary>
    public Periodo? Periodo { get; private set; }
    
    /// <summary>Referencia al usuario con privilegios que registró o auditó la nota.</summary>
    public Usuario? Auditor { get; private set; }

    /// <summary>Constructor vacío para EF Core.</summary>
    protected HistorialNota() { }

    /// <summary>
    /// Crea un nuevo registro de nota evaluando automáticamente el estado de aprobación.
    /// </summary>
    public HistorialNota(Guid inscripcionId, Guid materiaId, Guid periodoId, decimal? notaFinal, short intentos, Guid? registradoPor)
    {
        if (notaFinal.HasValue && (notaFinal < 0 || notaFinal > 100))
            throw new ArgumentException("La nota final debe estar entre 0 y 100 puntos.");

        if (intentos <= 0)
            throw new ArgumentException("El número de intentos debe ser al menos 1.");

        Id = Guid.NewGuid();
        InscripcionId = inscripcionId;
        MateriaId = materiaId;
        PeriodoId = periodoId;
        NotaFinal = notaFinal;
        Intentos = intentos;
        RegistradoPor = registradoPor;
        RegistradoEn = DateTimeOffset.UtcNow;

        // Lógica de negocio (Evaluación sobre 100 puntos, aprueba con 51)
        EvaluarEstadoInterno();
    }

    /// <summary>
    /// Actualiza la calificación y recalcula el estado.
    /// </summary>
    public void ActualizarNota(decimal nuevaNota, Guid modificadorId)
    {
        if (nuevaNota < 0 || nuevaNota > 100)
            throw new ArgumentException("La nota final debe estar entre 0 y 100 puntos.");

        NotaFinal = nuevaNota;
        RegistradoPor = modificadorId;
        RegistradoEn = DateTimeOffset.UtcNow;
        
        EvaluarEstadoInterno();
    }

    private void EvaluarEstadoInterno()
    {
        if (!NotaFinal.HasValue)
        {
            EstadoCurso = "EN_CURSO";
            return;
        }

        EstadoCurso = NotaFinal.Value >= 51 ? "APROBADO" : "REPROBADO";
    }
}