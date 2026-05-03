using System;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa una entidad educativa (Universidad, Instituto, Colegio) que utiliza la plataforma.
/// Base del sistema Multi-Tenant.
/// </summary>
public class Institucion
{
    /// <summary>Identificador único de la institución.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Nombre completo y oficial.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Acrónimo o nombre corto único (Ej. UMSA, UPDS).</summary>
    public string NombreCorto { get; private set; } = string.Empty;
    
    /// <summary>URL del logotipo oficial.</summary>
    public string? LogoUrl { get; private set; }
    
    /// <summary>Dominio de correo institucional para validaciones (Ej. umsa.bo).</summary>
    public string? DominioEmail { get; private set; }
    
    /// <summary>País de origen de la institución.</summary>
    public string? Pais { get; private set; }
    
    /// <summary>Zona horaria por defecto para la programación de citas y periodos.</summary>
    public string ZonaHoraria { get; private set; } = "America/La_Paz";
    
    /// <summary>Estado de operación en la plataforma.</summary>
    public bool Activa { get; private set; } = true;
    
    /// <summary>Fecha de registro de la institución.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Constructor vacío para Entity Framework Core.</summary>
    protected Institucion() { }

    /// <summary>
    /// Constructor de Dominio. Blinda la creación exigiendo los campos mínimos vitales.
    /// </summary>
    public Institucion(string nombre, string nombreCorto, string? logoUrl, string? dominioEmail, string? pais, string? zonaHoraria)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        NombreCorto = nombreCorto.ToUpperInvariant(); // Normalizamos el acrónimo a mayúsculas
        LogoUrl = logoUrl;
        DominioEmail = dominioEmail?.ToLowerInvariant();
        Pais = pais;
        ZonaHoraria = string.IsNullOrWhiteSpace(zonaHoraria) ? "America/La_Paz" : zonaHoraria;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Inactiva a la institución y todos sus accesos vinculados.</summary>
    public void Desactivar()
    {
        Activa = false;
    }
}