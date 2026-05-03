using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Analitica;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Analitica;

/// <summary>
/// Configuración Fluent API para la tabla analitica.eventos_telemetria.
/// Diseñada para inserciones masivas y análisis Big Data.
/// </summary>
public class EventoTelemetriaConfiguration : IEntityTypeConfiguration<EventoTelemetria>
{
    public void Configure(EntityTypeBuilder<EventoTelemetria> builder)
    {
        builder.ToTable("eventos_telemetria", "analitica");

        // PK Numérica de Alto Rendimiento
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityByDefaultColumn(); // Genera un BIGSERIAL en PostgreSQL

        builder.Property(e => e.TipoEvento).HasMaxLength(100).IsRequired();
        builder.Property(e => e.EntidadTipo).HasMaxLength(50);
        
        // Mapeo a JSONB para consultas NoSQL-like dentro de Postgres
        builder.Property(e => e.Metadata).HasColumnType("jsonb");

        // Índice compuesto crítico para rastrear la actividad de un usuario cronológicamente
        builder.HasIndex(e => new { e.UsuarioId, e.RegistradoEn }).IsDescending(false, true);
    }
}