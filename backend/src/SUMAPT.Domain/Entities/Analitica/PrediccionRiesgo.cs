using System;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Domain.Entities.Analitica;

/// <summary>
/// Representa el resultado del motor de Inteligencia Artificial sobre el riesgo de deserción de un estudiante en un periodo.
/// </summary>
public class PrediccionRiesgo
{
    /// <summary>Identificador único del registro de predicción.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID de la inscripción evaluada.</summary>
    public Guid InscripcionId { get; private set; }
    
    /// <summary>Puntuación matemática pura del algoritmo (Valor de 0.0000 a 1.0000).</summary>
    public decimal ScoreRiesgo { get; private set; }
    
    /// <summary>Clasificación categórica del riesgo (Ej. BAJO, MEDIO, ALTO, CRITICO).</summary>
    public string NivelRiesgo { get; private set; } = string.Empty;
    
    /// <summary>Explicabilidad del modelo: JSON con los pesos o motivos que llevaron a este score.</summary>
    public string Factores { get; private set; } = string.Empty;
    
    /// <summary>Firma de la versión del algoritmo utilizado (Ej. "XGBoost-v2.1").</summary>
    public string VersionModelo { get; private set; } = string.Empty;
    
    /// <summary>Sello de tiempo en el que el motor generó este cálculo.</summary>
    public DateTimeOffset GeneradoEn { get; private set; }
    
    /// <summary>Fecha de caducidad de esta predicción, requiriendo un nuevo cálculo posterior a ella.</summary>
    public DateTimeOffset VigenteHasta { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia la inscripción académica base.</summary>
    public Inscripcion? Inscripcion { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected PrediccionRiesgo() { }

    /// <summary>
    /// Construye y blinda matemáticamente el resultado predictivo.
    /// </summary>
    public PrediccionRiesgo(Guid inscripcionId, decimal scoreRiesgo, string nivelRiesgo, string factores, string versionModelo, DateTimeOffset vigenteHasta)
    {
        if (scoreRiesgo < 0 || scoreRiesgo > 1)
            throw new ArgumentException("El score de riesgo debe ser un valor probabilístico estricto entre 0 y 1.");

        if (string.IsNullOrWhiteSpace(nivelRiesgo))
            throw new ArgumentException("La etiqueta de nivel de riesgo no puede estar vacía.");

        Id = Guid.NewGuid();
        InscripcionId = inscripcionId;
        ScoreRiesgo = scoreRiesgo;
        NivelRiesgo = nivelRiesgo.ToUpperInvariant();
        Factores = factores;
        VersionModelo = versionModelo;
        GeneradoEn = DateTimeOffset.UtcNow;
        VigenteHasta = vigenteHasta;
    }
}