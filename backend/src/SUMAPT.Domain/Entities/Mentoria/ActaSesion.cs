using System;

namespace SUMAPT.Domain.Entities.Mentoria;

/// <summary>
/// Representa el registro oficial o bitácora de una sesión de mentoría finalizada.
/// </summary>
public class ActaSesion
{
    /// <summary>Identificador único del acta.</summary>
    public Guid Id { get; private set; }

    /// <summary>ID de la cita a la que pertenece esta acta (Relación estricta 1:1).</summary>
    public Guid CitaId { get; private set; }

    /// <summary>Resumen detallado de los temas abordados durante la sesión.</summary>
    public string TemasTratados { get; private set; } = string.Empty;

    /// <summary>Tareas o compromisos acordados por el estudiante (opcional).</summary>
    public string? Compromisos { get; private set; }

    /// <summary>Notas privadas u observaciones del mentor (opcional).</summary>
    public string? Observaciones { get; private set; }

    /// <summary>Evaluación subjetiva del mentor sobre el riesgo de deserción del estudiante.</summary>
    public string? NivelRiesgoPercibido { get; private set; }

    /// <summary>Fecha y hora en la que se registró el acta.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia a la cita original que originó esta acta.</summary>
    public Cita? Cita { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected ActaSesion() { }

    /// <summary>
    /// Construye una nueva acta validando que la información vital esté presente.
    /// </summary>
    public ActaSesion(Guid citaId, string temasTratados, string? compromisos, string? observaciones, string? nivelRiesgoPercibido)
    {
        if (string.IsNullOrWhiteSpace(temasTratados))
            throw new ArgumentException("Los temas tratados son obligatorios para cerrar el acta de una sesión.");

        Id = Guid.NewGuid();
        CitaId = citaId;
        TemasTratados = temasTratados;
        Compromisos = compromisos;
        Observaciones = observaciones;
        NivelRiesgoPercibido = nivelRiesgoPercibido?.ToUpperInvariant();
        CreadoEn = DateTimeOffset.UtcNow;
    }
}