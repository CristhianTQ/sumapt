using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Mentoria;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Mentoria;

/// <summary>
/// Configuración Fluent API para la tabla mentoria.citas.
/// </summary>
public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("citas", "mentoria");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Modalidad).HasMaxLength(20).IsRequired();
        builder.Property(c => c.Estado).HasMaxLength(30).IsRequired();

        // Restricción de Integridad
        builder.ToTable(t => t.HasCheckConstraint("CK_Cita_Fechas", "\"FechaHoraFin\" > \"FechaHoraIni\""));

        // Relaciones Críticas
        builder.HasOne(c => c.Mentor)
               .WithMany()
               .HasForeignKey(c => c.MentorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Estudiante)
               .WithMany()
               .HasForeignKey(c => c.EstudianteId)
               .OnDelete(DeleteBehavior.Restrict);

        // Relación Opcional (Nullable)
        builder.HasOne(c => c.Inscripcion)
               .WithMany()
               .HasForeignKey(c => c.InscripcionId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}