using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Auditoria;

/// <summary>
/// Representa un registro inmutable en la bitácora del sistema (Ledger).
/// Captura quién hizo qué, cuándo, y cómo cambiaron los datos.
/// </summary>
public class LogAccion
{
    /// <summary>Identificador secuencial del registro (BIGSERIAL).</summary>
    public long Id { get; private set; }
    
    /// <summary>ID del usuario que ejecutó la acción (null si fue el sistema).</summary>
    public Guid? UsuarioId { get; private set; }
    
    /// <summary>Rol bajo el cual operaba el usuario al momento de la acción.</summary>
    public string? RolActivo { get; private set; }
    
    /// <summary>Nombre técnico de la acción ejecutada (Ej. ACTUALIZAR_NOTA, CREAR_USUARIO).</summary>
    public string Accion { get; private set; } = string.Empty;
    
    /// <summary>Tabla o módulo afectado (Ej. HistorialNota, PerfilMentor).</summary>
    public string EntidadTipo { get; private set; } = string.Empty;
    
    /// <summary>Llave primaria del registro afectado (en formato texto para soportar UUID o Int).</summary>
    public string? EntidadId { get; private set; }
    
    /// <summary>Estado de los datos ANTES de la modificación (JSON).</summary>
    public string? DatosAntes { get; private set; }
    
    /// <summary>Estado de los datos DESPUÉS de la modificación (JSON).</summary>
    public string? DatosDespues { get; private set; }
    
    /// <summary>Dirección IP desde la que se originó la petición.</summary>
    public string? IpOrigen { get; private set; }
    
    /// <summary>Navegador web o cliente que realizó la petición.</summary>
    public string? UserAgent { get; private set; }
    
    /// <summary>Resultado de la operación (EXITOSO, FALLIDO, DENEGADO).</summary>
    public string Resultado { get; private set; } = "EXITOSO";
    
    /// <summary>Stack trace o mensaje técnico si la operación falló.</summary>
    public string? DetalleError { get; private set; }
    
    /// <summary>Sello de tiempo absoluto e inmutable de la ejecución.</summary>
    public DateTimeOffset EjecutadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia de navegación opcional hacia el usuario responsable.</summary>
    public Usuario? Usuario { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected LogAccion() { }

    /// <summary>
    /// Construye una entrada de bitácora garantizando que los datos mínimos de rastreo existan.
    /// </summary>
    public LogAccion(Guid? usuarioId, string? rolActivo, string accion, string entidadTipo, string? entidadId, 
                     string? datosAntes, string? datosDespues, string? ipOrigen, string? userAgent, 
                     string resultado, string? detalleError)
    {
        if (string.IsNullOrWhiteSpace(accion))
            throw new ArgumentException("La acción auditada no puede estar vacía.");

        if (string.IsNullOrWhiteSpace(entidadTipo))
            throw new ArgumentException("El tipo de entidad afectada es obligatorio para la auditoría.");

        UsuarioId = usuarioId;
        RolActivo = rolActivo;
        Accion = accion.ToUpperInvariant();
        EntidadTipo = entidadTipo;
        EntidadId = entidadId;
        DatosAntes = datosAntes;
        DatosDespues = datosDespues;
        IpOrigen = ipOrigen;
        UserAgent = userAgent;
        Resultado = string.IsNullOrWhiteSpace(resultado) ? "EXITOSO" : resultado.ToUpperInvariant();
        DetalleError = detalleError;
        EjecutadoEn = DateTimeOffset.UtcNow;
    }
}