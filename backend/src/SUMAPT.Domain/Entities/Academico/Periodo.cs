using System;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa un lapso de tiempo académico (Ej. Semestre I-2026, Gestión Anual 2026).
/// </summary>
public class Periodo
{
    /// <summary>Identificador único del periodo.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Llave foránea hacia la Institución a la que pertenece.</summary>
    public Guid InstitucionId { get; private set; }
    
    /// <summary>Nombre descriptivo del periodo.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Fecha oficial de inicio de clases.</summary>
    public DateOnly FechaInicio { get; private set; }
    
    /// <summary>Fecha oficial de finalización de clases.</summary>
    public DateOnly FechaFin { get; private set; }
    
    /// <summary>Indica si el periodo está habilitado para operaciones.</summary>
    public bool Activo { get; private set; } = true;
    
    /// <summary>Fecha de creación del registro.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Propiedad de navegación para Entity Framework.</summary>
    public Institucion? Institucion { get; private set; }

    /// <summary>Constructor vacío requerido por el ORM.</summary>
    protected Periodo() { }

    /// <summary>
    /// Construye un periodo garantizando su integridad desde la creación.
    /// </summary>
    public Periodo(Guid institucionId, string nombre, DateOnly fechaInicio, DateOnly fechaFin)
    {
        if (fechaFin <= fechaInicio)
            throw new ArgumentException("La fecha de fin debe ser estrictamente mayor a la de inicio.");

        Id = Guid.NewGuid();
        InstitucionId = institucionId;
        Nombre = nombre;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Desactiva el periodo académicamente.</summary>
    public void Desactivar() => Activo = false;
}