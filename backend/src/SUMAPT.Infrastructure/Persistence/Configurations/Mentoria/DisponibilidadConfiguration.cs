using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Mentoria;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Mentoria;

/// <summary>
/// Configuración Fluent API para la tabla mentoria.disponibilidad.
/// </summary>
public class DisponibilidadConfiguration : IEntityTypeConfiguration<Disponibilidad>
{
    public void Configure(EntityTypeBuilder<Disponibilidad> builder)
    {
        builder.ToTable("disponibilidad", "mentoria");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(d => d.Modalidad).HasMaxLength(20).IsRequired();

        // El driver Npgsql mapeará 'TimeSpan' al tipo de dato nativo 'time' de Postgres.
        builder.Property(d => d.HoraInicio).HasColumnType("time").IsRequired();
        builder.Property(d => d.HoraFin).HasColumnType("time").IsRequired();

        // Restricción de Integridad (CHECK) directamente en la Base de Datos
        builder.ToTable(t => t.HasCheckConstraint("CK_Disponibilidad_Horas", "\"HoraFin\" > \"HoraInicio\""));

        // Relaciones
        builder.HasOne(d => d.Mentor)
               .WithMany()
               .HasForeignKey(d => d.MentorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}