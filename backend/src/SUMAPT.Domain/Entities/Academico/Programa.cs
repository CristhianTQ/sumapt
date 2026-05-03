using System;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa una carrera o programa de estudios (Ej. Ingeniería de Sistemas).
/// </summary>
public class Programa
{
    /// <summary>Identificador único del programa.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID de la Institución a la que pertenece.</summary>
    public Guid InstitucionId { get; private set; }
    
    /// <summary>ID del modelo académico que rige su estructura temporal.</summary>
    public Guid ModeloId { get; private set; }
    
    /// <summary>Nombre oficial de la carrera o programa.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Código interno o sigla (Ej. SIS-100, INF).</summary>
    public string? Codigo { get; private set; }
    
    /// <summary>Duración total expresada en la cantidad de periodos (Ej. 10 semestres).</summary>
    public int DuracionPeriodos { get; private set; }
    
    /// <summary>Indica si el programa está habilitado para recibir inscripciones.</summary>
    public bool Activo { get; private set; } = true;
    
    /// <summary>Fecha de registro original.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Propiedad de navegación hacia la Institución.</summary>
    public Institucion? Institucion { get; private set; }
    
    /// <summary>Propiedad de navegación hacia el Modelo Académico.</summary>
    public ModeloAcademico? ModeloAcademico { get; private set; }

    /// <summary>Constructor vacío para Entity Framework Core.</summary>
    protected Programa() { }

    /// <summary>
    /// Constructor de Dominio para garantizar un estado válido inicial.
    /// </summary>
    public Programa(Guid institucionId, Guid modeloId, string nombre, string? codigo, int duracionPeriodos)
    {
        if (duracionPeriodos <= 0)
            throw new ArgumentException("La duración en periodos debe ser mayor a cero.");

        Id = Guid.NewGuid();
        InstitucionId = institucionId;
        ModeloId = modeloId;
        Nombre = nombre;
        Codigo = codigo?.ToUpperInvariant(); // Estandarizamos el código
        DuracionPeriodos = duracionPeriodos;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Da de baja el programa de forma lógica.</summary>
    public void Desactivar() => Activo = false;
}