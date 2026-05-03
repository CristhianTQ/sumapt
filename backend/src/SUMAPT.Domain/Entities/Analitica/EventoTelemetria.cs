using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Analitica;

/// <summary>
/// Representa un evento de rastreo (telemetría) para el análisis de comportamiento.
/// Es la fuente principal de alimentación para los modelos de Inteligencia Artificial.
/// </summary>
public class EventoTelemetria
{
    /// <summary>Identificador secuencial del evento (BIGSERIAL en BD).</summary>
    public long Id { get; private set; }
    
    /// <summary>ID del usuario que generó el evento de interacción.</summary>
    public Guid UsuarioId { get; private set; }
    
    /// <summary>Opcional: ID de la sesión web activa para trazabilidad de la jornada.</summary>
    public Guid? SesionId { get; private set; }
    
    /// <summary>Clasificación estructurada de la acción (Ej. DESCARGA_MATERIAL, LOGIN_EXITOSO).</summary>
    public string TipoEvento { get; private set; } = string.Empty;
    
    /// <summary>Opcional: Entidad o módulo con el que interactuó (Ej. MATERIA, FORO).</summary>
    public string? EntidadTipo { get; private set; }
    
    /// <summary>Opcional: ID exacto del registro afectado o visitado.</summary>
    public Guid? EntidadId { get; private set; }
    
    /// <summary>Carga útil (Payload) en formato JSON estructurado con variables de entorno de la acción.</summary>
    public string? Metadata { get; private set; }
    
    /// <summary>Sello de tiempo absoluto de la interacción.</summary>
    public DateTimeOffset RegistradoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia física hacia el usuario emisor.</summary>
    public Usuario? Usuario { get; private set; }
    
    /// <summary>Referencia física hacia la sesión capturada.</summary>
    public Sesion? Sesion { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected EventoTelemetria() { }

    /// <summary>
    /// Construye un registro inmutable de telemetría. 
    /// El ID no se genera aquí; Postgres lo asignará mediante BIGSERIAL.
    /// </summary>
    public EventoTelemetria(Guid usuarioId, Guid? sesionId, string tipoEvento, string? entidadTipo, Guid? entidadId, string? metadata)
    {
        if (string.IsNullOrWhiteSpace(tipoEvento))
            throw new ArgumentException("El tipo de evento no puede estar vacío para la telemetría.");

        UsuarioId = usuarioId;
        SesionId = sesionId;
        TipoEvento = tipoEvento.ToUpperInvariant();
        EntidadTipo = entidadTipo?.ToUpperInvariant();
        EntidadId = entidadId;
        Metadata = metadata;
        RegistradoEn = DateTimeOffset.UtcNow;
    }
}