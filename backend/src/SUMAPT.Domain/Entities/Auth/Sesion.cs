using System;

namespace SUMAPT.Domain.Entities.Auth;

/// <summary>
/// Representa una sesión activa de un usuario en el sistema.
/// Permite gestionar auditorías y cierres de sesión forzados (Single Sign-Out).
/// Mapea a la tabla auth.sesiones.
/// </summary>
public class Sesion
{
    /// <summary>Identificador interno único de la sesión.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID del usuario propietario de la sesión.</summary>
    public Guid UsuarioId { get; private set; }
    
    /// <summary>ID de sesión generado por Keycloak, vital para el logout unificado.</summary>
    public string KeycloakSid { get; private set; } = string.Empty;
    
    /// <summary>Dirección IP desde la que se originó la conexión.</summary>
    public string? IpOrigen { get; private set; }
    
    /// <summary>Información sobre el navegador o dispositivo utilizado.</summary>
    public string? UserAgent { get; private set; }
    
    /// <summary>Fecha y hora exacta en la que se generó el token inicial.</summary>
    public DateTimeOffset IniciadaEn { get; private set; }
    
    /// <summary>Fecha y hora en la que la sesión fue revocada o expiró naturalmente.</summary>
    public DateTimeOffset? ExpiradaEn { get; private set; }
    
    /// <summary>Bandera para determinar si el token sigue siendo válido operativamente.</summary>
    public bool Activa { get; private set; } = true;

    /// <summary>Propiedad de navegación hacia la entidad Usuario.</summary>
    public Usuario? Usuario { get; private set; }

    /// <summary>
    /// Constructor requerido por EF Core.
    /// </summary>
    protected Sesion() { }

    /// <summary>
    /// Registra el inicio de una nueva sesión de acceso al ecosistema.
    /// </summary>
    public Sesion(Guid usuarioId, string keycloakSid, string? ipOrigen, string? userAgent)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        KeycloakSid = keycloakSid;
        IpOrigen = ipOrigen;
        UserAgent = userAgent;
        IniciadaEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Invalida explícitamente la sesión, cortando el acceso del usuario.
    /// </summary>
    public void Finalizar()
    {
        Activa = false;
        ExpiradaEn = DateTimeOffset.UtcNow;
    }
}