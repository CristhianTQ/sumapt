using System;

namespace SUMAPT.Domain.Entities.Academico;

/// <summary>
/// Representa una asignatura, curso o materia que forma parte de un Programa Académico.
/// </summary>
public class Materia
{
    /// <summary>Identificador único de la materia.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Llave foránea hacia la carrera o programa al que pertenece.</summary>
    public Guid ProgramaId { get; private set; }
    
    /// <summary>Nombre oficial de la asignatura.</summary>
    public string Nombre { get; private set; } = string.Empty;
    
    /// <summary>Código o sigla oficial de la materia (Ej. SIS-100).</summary>
    public string Codigo { get; private set; } = string.Empty;
    
    /// <summary>Valor académico en créditos.</summary>
    public short Creditos { get; private set; }
    
    /// <summary>Semestre, año o ciclo en el que se sugiere cursar (Opcional).</summary>
    public short? PeriodoSugerido { get; private set; }
    
    /// <summary>Indica si la materia sigue activa en la malla curricular actual.</summary>
    public bool Activa { get; private set; } = true;
    
    /// <summary>Fecha de creación del registro.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    /// <summary>Propiedad de navegación para Entity Framework Core.</summary>
    public Programa? Programa { get; private set; }

    /// <summary>Constructor vacío requerido por el ORM.</summary>
    protected Materia() { }

    /// <summary>
    /// Constructor de Dominio para inicializar una materia en un estado válido.
    /// </summary>
    public Materia(Guid programaId, string nombre, string codigo, short creditos, short? periodoSugerido)
    {
        if (creditos <= 0)
            throw new ArgumentException("Los créditos académicos deben ser mayores a cero.");

        Id = Guid.NewGuid();
        ProgramaId = programaId;
        Nombre = nombre;
        Codigo = codigo.ToUpperInvariant(); // Normalización estricta de códigos
        Creditos = creditos;
        PeriodoSugerido = periodoSugerido;
        CreadoEn = DateTimeOffset.UtcNow;
    }

    /// <summary>Marca la materia como inactiva (baja lógica).</summary>
    public void Desactivar() => Activa = false;
}