using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.inscripciones.
/// </summary>
public class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
{
    public void Configure(EntityTypeBuilder<Inscripcion> builder)
    {
        builder.ToTable("inscripciones", "academico");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(i => i.Estado).HasMaxLength(30).IsRequired();

        // Regla de Negocio Crítica (Diccionario SQL): 
        // Un estudiante no puede inscribirse dos veces al mismo programa en el mismo periodo.
        builder.HasIndex(i => new { i.EstudianteId, i.ProgramaId, i.PeriodoId }).IsUnique();

        // Relaciones
        builder.HasOne(i => i.Estudiante)
               .WithMany()
               .HasForeignKey(i => i.EstudianteId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Programa)
               .WithMany()
               .HasForeignKey(i => i.ProgramaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Periodo)
               .WithMany()
               .HasForeignKey(i => i.PeriodoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}