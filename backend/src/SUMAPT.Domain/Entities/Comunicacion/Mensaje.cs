using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Comunicacion;

/// <summary>
/// Representa un texto individual enviado dentro de una Conversación.
/// </summary>
public class Mensaje
{
    /// <summary>Identificador único del mensaje.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID de la conversación a la que pertenece este mensaje.</summary>
    public Guid ConversacionId { get; private set; }
    
    /// <summary>ID del usuario que envió el mensaje.</summary>
    public Guid RemitenteId { get; private set; }
    
    /// <summary>Contenido de texto del mensaje.</summary>
    public string Contenido { get; private set; } = string.Empty;
    
    /// <summary>Indica si el mensaje ha sido leído por el destinatario.</summary>
    public bool Leido { get; private set; }
    
    /// <summary>Fecha y hora exacta del envío.</summary>
    public DateTimeOffset EnviadoEn { get; private set; }
    
    /// <summary>Fecha y hora en que el mensaje fue marcado como leído.</summary>
    public DateTimeOffset? LeidoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia el canal de chat.</summary>
    public Conversacion? Conversacion { get; private set; }
    
    /// <summary>Referencia hacia el usuario emisor.</summary>
    public Usuario? Remitente { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected Mensaje() { }

    /// <summary>
    /// Construye un nuevo mensaje validando que no esté vacío.
    /// </summary>
    /// <param name="conversacionId">ID de la conversación destino.</param>
    /// <param name="remitenteId">ID del usuario que emite el texto.</param>
    /// <param name="contenido">El texto del mensaje.</param>
    public Mensaje(Guid conversacionId, Guid remitenteId, string contenido)
    {
        if (string.IsNullOrWhiteSpace(contenido))
            throw new ArgumentException("El contenido del mensaje no puede estar vacío.");

        Id = Guid.NewGuid();
        ConversacionId = conversacionId;
        RemitenteId = remitenteId;
        Contenido = contenido;
        Leido = false;
        EnviadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Marca el mensaje como leído (Double Blue Tick) registrando el momento exacto.</summary>
    public void MarcarComoLeido()
    {
        if (Leido) return;
        Leido = true;
        LeidoEn = DateTimeOffset.UtcNow;
    }
}