using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Comunicacion;

/// <summary>
/// Representa una alerta o mensaje de sistema dirigido a un usuario específico.
/// </summary>
public class Notificacion
{
    /// <summary>Identificador único de la notificación.</summary>
    public Guid Id { get; private set; }

    /// <summary>ID del usuario al que va dirigida la alerta.</summary>
    public Guid DestinatarioId { get; private set; }

    /// <summary>Clasificación de la alerta (Ej. SISTEMA, MENTORIA, RIESGO).</summary>
    public string Tipo { get; private set; } = string.Empty;

    /// <summary>Título corto y descriptivo.</summary>
    public string Titulo { get; private set; } = string.Empty;

    /// <summary>Contenido detallado del mensaje.</summary>
    public string Cuerpo { get; private set; } = string.Empty;

    /// <summary>Indica si el usuario ya visualizó la alerta.</summary>
    public bool Leida { get; private set; }

    /// <summary>Datos adicionales serializados en JSON para redirecciones en el Frontend.</summary>
    public string? DatosExtra { get; private set; }

    /// <summary>Fecha y hora de emisión.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Fecha y hora en la que el usuario marcó la notificación como leída.</summary>
    public DateTimeOffset? LeidaEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia al usuario destinatario.</summary>
    public Usuario? Destinatario { get; private set; }

    /// <summary>Constructor vacío para EF Core.</summary>
    protected Notificacion() { }

    /// <summary>
    /// Construye una nueva alerta del sistema en estado no leída.
    /// </summary>
    public Notificacion(Guid destinatarioId, string tipo, string titulo, string cuerpo, string? datosExtra)
    {
        Id = Guid.NewGuid();
        DestinatarioId = destinatarioId;
        Tipo = tipo.ToUpperInvariant();
        Titulo = titulo;
        Cuerpo = cuerpo;
        DatosExtra = datosExtra;
        Leida = false;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Cambia el estado de la notificación a leída y registra la marca de tiempo.
    /// </summary>
    public void MarcarComoLeida()
    {
        if (Leida) return; // Si ya estaba leída, no hacemos nada
        
        Leida = true;
        LeidaEn = DateTimeOffset.UtcNow;
    }
}