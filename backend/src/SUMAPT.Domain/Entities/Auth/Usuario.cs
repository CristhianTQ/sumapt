using System;

namespace SUMAPT.Domain.Entities.Auth;

/// <summary>
/// Entidad principal del ecosistema (Estudiante, Docente, Mentor, Admin).
/// Mapea a la tabla auth.usuarios.
/// </summary>
public class Usuario
{
    /// <summary>Llave primaria local del sistema.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Identificador único proveniente del servidor de identidad (Keycloak).</summary>
    public Guid KeycloakId { get; private set; }
    
    /// <summary>Correo electrónico único del usuario.</summary>
    public string Email { get; private set; } = string.Empty;
    
    /// <summary>Nombre de pila del usuario.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Apellido o apellidos del usuario.</summary>
    public string Apellido { get; private set; } = string.Empty;
    
    /// <summary>Número de teléfono de contacto (opcional).</summary>
    public string? Telefono { get; private set; }
    
    /// <summary>URL de la imagen de perfil (opcional).</summary>
    public string? AvatarUrl { get; private set; }
    
    /// <summary>Zona horaria por defecto asumiendo el contexto de Bolivia.</summary>
    public string ZonaHoraria { get; private set; } = "America/La_Paz";
    
    /// <summary>Idioma de preferencia en la interfaz de la plataforma.</summary>
    public string Idioma { get; private set; } = "es";
    
    /// <summary>Indica si el usuario tiene permiso para acceder al sistema local.</summary>
    public bool Activo { get; private set; } = true;
    
    /// <summary>Fecha y hora del último inicio de sesión exitoso.</summary>
    public DateTimeOffset? UltimoAccesoEn { get; private set; }
    
    /// <summary>Fecha de registro original en el sistema.</summary>
    public DateTimeOffset CreadoEn { get; private set; }
    
    /// <summary>Fecha de la última modificación de sus datos de perfil.</summary>
    public DateTimeOffset ActualizadoEn { get; private set; }

    /// <summary>
    /// Constructor vacío requerido estrictamente por Entity Framework Core.
    /// </summary>
    protected Usuario() { }

    /// <summary>
    /// Constructor de Dominio. 
    /// Exige los 8 argumentos necesarios para garantizar que un usuario nazca en un estado válido.
    /// </summary>
    /// <param name="keycloakId">ID del proveedor de identidad.</param>
    /// <param name="email">Correo electrónico.</param>
    /// <param name="nombre">Nombre.</param>
    /// <param name="apellido">Apellido.</param>
    /// <param name="telefono">Teléfono.</param>
    /// <param name="avatarUrl">Avatar.</param>
    /// <param name="zonaHoraria">Zona Horaria.</param>
    /// <param name="idioma">Idioma.</param>
    public Usuario(Guid keycloakId, string email, string nombre, string apellido, string? telefono, string? avatarUrl, string zonaHoraria, string idioma)
    {
        Id = Guid.NewGuid();
        KeycloakId = keycloakId;
        Email = email;
        Nombre = nombre;
        Apellido = apellido;
        Telefono = telefono;
        AvatarUrl = avatarUrl;
        
        // Asignación con protección contra nulos o vacíos
        ZonaHoraria = string.IsNullOrWhiteSpace(zonaHoraria) ? "America/La_Paz" : zonaHoraria;
        Idioma = string.IsNullOrWhiteSpace(idioma) ? "es" : idioma;
        
        CreadoEn = DateTimeOffset.UtcNow;
        ActualizadoEn = DateTimeOffset.UtcNow;
    }

    // ==========================================
    // MÉTODOS DE COMPORTAMIENTO (Rich Model)
    // ==========================================

    /// <summary>
    /// Actualiza el sello de tiempo indicando actividad reciente en la plataforma.
    /// </summary>
    public void RegistrarAcceso()
    {
        UltimoAccesoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Bloquea el acceso local del usuario al sistema.
    /// </summary>
    public void DesactivarCuenta()
    {
        Activo = false;
        ActualizadoEn = DateTimeOffset.UtcNow;
    }
}