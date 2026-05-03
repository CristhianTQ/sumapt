using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Mentoria;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Mentoria;

/// <summary>
/// Configuración Fluent API para la tabla mentoria.actas_sesion.
/// </summary>
public class ActaSesionConfiguration : IEntityTypeConfiguration<ActaSesion>
{
    public void Configure(EntityTypeBuilder<ActaSesion> builder)
    {
        builder.ToTable("actas_sesion", "mentoria");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(a => a.TemasTratados).IsRequired();
        builder.Property(a => a.NivelRiesgoPercibido).HasMaxLength(20);

        // Relación 1:1 Estricta con Cita
        builder.HasIndex(a => a.CitaId).IsUnique();

        builder.HasOne(a => a.Cita)
               .WithOne()
               .HasForeignKey<ActaSesion>(a => a.CitaId)
               .OnDelete(DeleteBehavior.Restrict); // No se puede borrar una cita si ya tiene un acta legal
    }
}