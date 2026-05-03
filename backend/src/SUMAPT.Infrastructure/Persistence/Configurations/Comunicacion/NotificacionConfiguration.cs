using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Comunicacion;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Comunicacion;

/// <summary>
/// Configuración Fluent API para la tabla comunicacion.notificaciones.
/// </summary>
public class NotificacionConfiguration : IEntityTypeConfiguration<Notificacion>
{
    public void Configure(EntityTypeBuilder<Notificacion> builder)
    {
        builder.ToTable("notificaciones", "comunicacion");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(n => n.Tipo).HasMaxLength(50).IsRequired();
        builder.Property(n => n.Titulo).HasMaxLength(255).IsRequired();
        builder.Property(n => n.Cuerpo).IsRequired();

        // Mapeo Crítico: Transforma el string de C# en un JSONB nativo de PostgreSQL
        builder.Property(n => n.DatosExtra).HasColumnType("jsonb");

        builder.HasOne(n => n.Destinatario)
               .WithMany()
               .HasForeignKey(n => n.DestinatarioId)
               .OnDelete(DeleteBehavior.Cascade);

        // Índice optimizado para buscar notificaciones no leídas
        builder.HasIndex(n => n.DestinatarioId)
               .HasFilter("\"Leida\" = FALSE");
    }
}