using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SUMAPT.Domain.Entities.Auth;
using SUMAPT.Domain.Entities.Academico;
using SUMAPT.Domain.Entities.Mentoria;
using SUMAPT.Domain.Entities.Comunicacion;
using SUMAPT.Domain.Entities.Analitica;
using SUMAPT.Domain.Entities.Auditoria;

namespace SUMAPT.Infrastructure.Persistence;

/// <summary>
/// Contexto principal de Entity Framework Core.
/// Actúa como la Unidad de Trabajo y puente físico con PostgreSQL.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // ==========================================
    // DB SETS (Tablas expuestas al sistema)
    // ==========================================
    
    // Esquema: Auth
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
    public DbSet<Sesion> Sesiones => Set<Sesion>();

    // Esquema: Academico
    public DbSet<Institucion> Instituciones => Set<Institucion>();
    public DbSet<ModeloAcademico> ModelosAcademicos => Set<ModeloAcademico>();
    public DbSet<Programa> Programas => Set<Programa>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<Periodo> Periodos => Set<Periodo>();
    public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();
    public DbSet<HistorialNota> HistorialNotas => Set<HistorialNota>();

    // Esquema: Mentoria
    public DbSet<PerfilMentor> PerfilesMentor => Set<PerfilMentor>();
    public DbSet<Disponibilidad> Disponibilidades => Set<Disponibilidad>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<ActaSesion> ActasSesion => Set<ActaSesion>();

    // Esquema: Comunicacion
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
    public DbSet<Conversacion> Conversaciones => Set<Conversacion>();
    public DbSet<Mensaje> Mensajes => Set<Mensaje>();

    // Esquema: Analitica
    public DbSet<EventoTelemetria> EventosTelemetria => Set<EventoTelemetria>();
    public DbSet<PrediccionRiesgo> PrediccionesRiesgo => Set<PrediccionRiesgo>();
    public DbSet<MetricaAgregada> MetricasAgregadas => Set<MetricaAgregada>();

    // Esquema: Auditoria
    public DbSet<LogAccion> LogAcciones => Set<LogAccion>();

    /// <summary>
    /// Construye el modelo mapeando las configuraciones Fluent API.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Habilitar extensión criptográfica
        modelBuilder.HasPostgresExtension("pgcrypto");

        // Escanear dinámicamente las 22 configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}