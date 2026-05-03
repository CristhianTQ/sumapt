using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Comunicacion;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Comunicacion;

/// <summary>
/// Configuración Fluent API para la tabla comunicacion.conversaciones.
/// </summary>
public class ConversacionConfiguration : IEntityTypeConfiguration<Conversacion>
{
    public void Configure(EntityTypeBuilder<Conversacion> builder)
    {
        builder.ToTable("conversaciones", "comunicacion");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        // Regla: No pueden existir dos canales de chat duplicados entre las mismas dos personas
        builder.HasIndex(c => new { c.ParticipanteA, c.ParticipanteB }).IsUnique();

        // Regla: Un usuario no puede chatear consigo mismo
        builder.ToTable(t => t.HasCheckConstraint("CK_Conversacion_Distintos", "\"ParticipanteA\" <> \"ParticipanteB\""));

        builder.HasOne(c => c.UsuarioA)
               .WithMany()
               .HasForeignKey(c => c.ParticipanteA)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.UsuarioB)
               .WithMany()
               .HasForeignKey(c => c.ParticipanteB)
               .OnDelete(DeleteBehavior.Restrict);
    }
}