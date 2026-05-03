using System;

namespace SUMAPT.Domain.Entities.Auth;

/// <summary>
/// Define los roles de acceso dentro de la plataforma (ej. Administrador, Mentor, Estudiante).
/// Mapea a la tabla auth.roles.
/// </summary>
public class Rol
{
    /// <summary>Identificador único del rol.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Nombre único y normalizado del rol.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Descripción detallada de los permisos que otorga.</summary>
    public string? Descripcion { get; private set; }
    
    /// <summary>Indica si el rol puede ser asignado actualmente.</summary>
    public bool Activo { get; private set; } = true;
    
    /// <summary>Fecha de creación del registro del rol.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>
    /// Constructor protegido requerido por Entity Framework Core.
    /// </summary>
    protected Rol() { }

    /// <summary>
    /// Crea una nueva instancia de un Rol con sus datos iniciales.
    /// </summary>
    public Rol(string nombre, string? descripcion = null)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        Descripcion = descripcion;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Inhabilita el rol, previniendo futuras asignaciones sin eliminar el histórico.
    /// </summary>
    public void Desactivar()
    {
        Activo = false;
    }
}