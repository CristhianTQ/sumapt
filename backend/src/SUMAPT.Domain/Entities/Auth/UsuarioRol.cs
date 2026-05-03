using System;

namespace SUMAPT.Domain.Entities.Auth;

/// <summary>
/// Relación N:M entre Usuarios y Roles, con soporte para tenencia por institución académica.
/// Mapea a la tabla auth.usuario_roles.
/// </summary>
public class UsuarioRol
{
    /// <summary>Identificador principal de la asignación.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Identificador del usuario que recibe el rol.</summary>
    public Guid UsuarioId { get; private set; }
    
    /// <summary>Identificador del rol otorgado.</summary>
    public Guid RolId { get; private set; }
    
    /// <summary>Referencia a la institución, permite roles aislados por sede universitaria.</summary>
    public Guid? InstitucionId { get; private set; }
    
    /// <summary>Momento exacto en que se otorgó el privilegio.</summary>
    public DateTimeOffset AsignadoEn { get; private set; }
    
    /// <summary>Usuario administrador que autorizó la asignación, para trazabilidad.</summary>
    public Guid? AsignadoPorId { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN (Entity Framework)
    // ==========================================
    
    /// <summary>Referencia en memoria hacia la entidad Usuario.</summary>
    public Usuario? Usuario { get; private set; }
    
    /// <summary>Referencia en memoria hacia la entidad Rol.</summary>
    public Rol? Rol { get; private set; }

    /// <summary>
    /// Constructor vacío requerido por ORMs para hidratación de datos.
    /// </summary>
    protected UsuarioRol() { }

    /// <summary>
    /// Genera un vínculo seguro entre un usuario, un rol y opcionalmente su institución.
    /// </summary>
    public UsuarioRol(Guid usuarioId, Guid rolId, Guid? institucionId = null, Guid? asignadoPorId = null)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        RolId = rolId;
        InstitucionId = institucionId;
        AsignadoEn = DateTimeOffset.UtcNow;
        AsignadoPorId = asignadoPorId;
    }
}