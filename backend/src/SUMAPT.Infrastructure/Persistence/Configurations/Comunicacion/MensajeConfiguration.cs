using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Comunicacion;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Comunicacion;

/// <summary>
/// Configuración Fluent API para la tabla comunicacion.mensajes.
/// </summary>
public class MensajeConfiguration : IEntityTypeConfiguration<Mensaje>
{
    public void Configure(EntityTypeBuilder<Mensaje> builder)
    {
        builder.ToTable("mensajes", "comunicacion");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(m => m.Contenido).IsRequired();

        // Si se borra la conversación, se destruyen todos los mensajes (Cascade)
        builder.HasOne(m => m.Conversacion)
               .WithMany()
               .HasForeignKey(m => m.ConversacionId)
               .OnDelete(DeleteBehavior.Cascade);

        // No se puede borrar a un usuario si dejó mensajes en un canal de chat vivo
        builder.HasOne(m => m.Remitente)
               .WithMany()
               .HasForeignKey(m => m.RemitenteId)
               .OnDelete(DeleteBehavior.Restrict);

        // Índice para ordenar mensajes por fecha rápidamente
        builder.HasIndex(m => new { m.ConversacionId, m.EnviadoEn }).IsDescending(false, true);
    }
}