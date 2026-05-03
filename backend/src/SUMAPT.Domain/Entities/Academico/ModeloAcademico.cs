using System;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Define la estructura de tiempo de un programa de estudios (Semestral, Anual, Modular).
/// </summary>
public class ModeloAcademico
{
    /// <summary>Identificador único del modelo.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Llave foránea hacia la Institución dueña de este modelo.</summary>
    public Guid InstitucionId { get; private set; }
    
    /// <summary>Nombre descriptivo (Ej. Modelo Semestral Estándar).</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Clasificación técnica del tipo de modelo (Ej. SEMESTRAL, MODULAR).</summary>
    public string Tipo { get; private set; } = string.Empty;
    
    /// <summary>Cantidad de ciclos que ocurren en un año calendario.</summary>
    public int PeriodosPorAño { get; private set; } = 2;
    
    /// <summary>Indica si el modelo está disponible para crear nuevos programas.</summary>
    public bool Activo { get; private set; } = true;
    
    /// <summary>Fecha de registro original.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Propiedad de navegación para EF Core.</summary>
    public Institucion? Institucion { get; private set; }

    /// <summary>Constructor vacío requerido por el ORM.</summary>
    protected ModeloAcademico() { }

    /// <summary>
    /// Constructor de Dominio. Valida la lógica interna al momento de instanciar.
    /// </summary>
    public ModeloAcademico(Guid institucionId, string nombre, string tipo, int periodosPorAño)
    {
        if (periodosPorAño <= 0)
            throw new ArgumentException("Los periodos por año deben ser mayores a cero.");

        Id = Guid.NewGuid();
        InstitucionId = institucionId;
        Nombre = nombre;
        Tipo = tipo.ToUpperInvariant();
        PeriodosPorAño = periodosPorAño;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Deshabilita el modelo académico.</summary>
    public void Desactivar() => Activo = false;
}