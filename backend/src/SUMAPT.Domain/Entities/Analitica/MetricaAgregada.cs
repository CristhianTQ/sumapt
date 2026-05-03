using System;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Domain.Entities.Analitica;

/// <summary>
/// Representa un valor estadístico precalculado (KPI) para lectura ultrarrápida en Dashboards.
/// Evita recálculos costosos sobre grandes volúmenes de datos.
/// </summary>
public class MetricaAgregada
{
    /// <summary>Identificador único del registro de la métrica.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID de la institución a la que pertenece esta métrica general.</summary>
    public Guid InstitucionId { get; private set; }
    
    /// <summary>Opcional: ID del periodo académico si la métrica es temporal (Ej. Tasa de deserción 2026-1).</summary>
    public Guid? PeriodoId { get; private set; }
    
    /// <summary>Nombre clave o llave identificadora del KPI (Ej. TASA_DESERCION_GLOBAL, ALUMNOS_RIESGO_ALTO).</summary>
    public string TipoMetrica { get; private set; } = string.Empty;
    
    /// <summary>Valor numérico calculado (Permite hasta 4 decimales de precisión).</summary>
    public decimal Valor { get; private set; }
    
    /// <summary>Sello de tiempo indicando el momento exacto en que el worker realizó el cálculo.</summary>
    public DateTimeOffset CalculadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia la institución propietaria de los datos.</summary>
    public Institucion? Institucion { get; private set; }
    
    /// <summary>Referencia hacia el periodo de la evaluación (si aplica).</summary>
    public Periodo? Periodo { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected MetricaAgregada() { }

    /// <summary>
    /// Construye una nueva métrica precalculada asegurando la estandarización de la llave.
    /// </summary>
    public MetricaAgregada(Guid institucionId, Guid? periodoId, string tipoMetrica, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(tipoMetrica))
            throw new ArgumentException("El tipo de métrica no puede estar vacío.");

        Id = Guid.NewGuid();
        InstitucionId = institucionId;
        PeriodoId = periodoId;
        TipoMetrica = tipoMetrica.ToUpperInvariant();
        Valor = valor;
        CalculadoEn = DateTimeOffset.UtcNow;
    }
}