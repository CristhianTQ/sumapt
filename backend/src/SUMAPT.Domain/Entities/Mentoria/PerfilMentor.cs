using System;
using System.Collections.Generic;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Mentoria;

/// <summary>
/// Representa la extensión del perfil de un usuario que lo habilita para impartir mentorías.
/// </summary>
public class PerfilMentor
{
    /// <summary>Identificador único del perfil de mentor.</summary>
    public Guid Id { get; private set; }

    /// <summary>ID del usuario base asociado a este perfil (Relación 1:1).</summary>
    public Guid UsuarioId { get; private set; }

    /// <summary>Biografía, experiencia o presentación profesional del mentor.</summary>
    public string? Biografia { get; private set; }

    /// <summary>Lista de materias, tecnologías o áreas en las que el mentor es experto.</summary>
    public List<string> Especialidades { get; private set; } = new();

    /// <summary>Límite máximo de estudiantes concurrentes que el mentor acepta asesorar.</summary>
    public short MaxEstudiantes { get; private set; }

    /// <summary>Indica si el mentor está actualmente activo y visible para los estudiantes.</summary>
    public bool Activo { get; private set; } = true;

    /// <summary>Fecha de alta como mentor.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia hacia los datos de identidad del usuario.</summary>
    public Usuario? Usuario { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected PerfilMentor() { }

    /// <summary>
    /// Construye y valida un nuevo perfil de mentor.
    /// </summary>
    public PerfilMentor(Guid usuarioId, string? biografia, List<string> especialidades, short maxEstudiantes)
    {
        if (maxEstudiantes <= 0) 
            throw new ArgumentException("El límite máximo de estudiantes debe ser al menos 1.");

        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        Biografia = biografia;
        Especialidades = especialidades ?? new List<string>();
        MaxEstudiantes = maxEstudiantes;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Pausa temporalmente la visibilidad del mentor.</summary>
    public void PausarDisponibilidad() => Activo = false;
    
    /// <summary>Reactiva la visibilidad del mentor.</summary>
    public void ActivarDisponibilidad() => Activo = true;
}