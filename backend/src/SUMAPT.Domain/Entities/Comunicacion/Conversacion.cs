using System;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Domain.Entities.Comunicacion;

/// <summary>
/// Representa un canal de chat único e irrepetible entre dos usuarios.
/// </summary>
public class Conversacion
{
    /// <summary>Identificador único de la conversación.</summary>
    public Guid Id { get; private set; }
    
    /// <summary>ID del primer participante (ordenado alfanuméricamente).</summary>
    public Guid ParticipanteA { get; private set; }
    
    /// <summary>ID del segundo participante (ordenado alfanuméricamente).</summary>
    public Guid ParticipanteB { get; private set; }
    
    /// <summary>Fecha y hora en que se inició la conversación.</summary>
    public DateTimeOffset CreadoEn { get; private set; }

    // ==========================================
    // PROPIEDADES DE NAVEGACIÓN
    // ==========================================
    
    /// <summary>Referencia de navegación hacia el primer participante.</summary>
    public Usuario? UsuarioA { get; private set; }
    
    /// <summary>Referencia de navegación hacia el segundo participante.</summary>
    public Usuario? UsuarioB { get; private set; }

    /// <summary>Constructor vacío requerido por Entity Framework Core.</summary>
    protected Conversacion() { }

    /// <summary>
    /// Crea un canal de comunicación aplicando normalización de IDs para evitar duplicados bidireccionales.
    /// </summary>
    public Conversacion(Guid usuario1, Guid usuario2)
    {
        if (usuario1 == usuario2)
            throw new ArgumentException("Un usuario no puede iniciar una conversación consigo mismo.");

        Id = Guid.NewGuid();
        
        // TRUCO SENIOR: Ordenamos los GUIDs. 
        // Esto garantiza que el par (User1, User2) se guarde exactamente igual que (User2, User1).
        // Así el motor de base de datos puede aplicar el UNIQUE index sin fallar.
        if (usuario1.CompareTo(usuario2) < 0)
        {
            ParticipanteA = usuario1;
            ParticipanteB = usuario2;
        }
        else
        {
            ParticipanteA = usuario2;
            ParticipanteB = usuario1;
        }

        CreadoEn = DateTimeOffset.UtcNow;
    }
}