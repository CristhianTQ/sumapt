using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.historial_notas.
/// </summary>
public class HistorialNotaConfiguration : IEntityTypeConfiguration<HistorialNota>
{
    public void Configure(EntityTypeBuilder<HistorialNota> builder)
    {
        builder.ToTable("historial_notas", "academico");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasDefaultValueSql("gen_random_uuid()");

        // Definición de precisión numérica (5 dígitos totales, 2 decimales. Ej: 100.00)
        builder.Property(h => h.NotaFinal).HasColumnType("numeric(5,2)");
        builder.Property(h => h.EstadoCurso).HasMaxLength(30).IsRequired();

        // Relaciones principales
        builder.HasOne(h => h.Inscripcion)
               .WithMany()
               .HasForeignKey(h => h.InscripcionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.Materia)
               .WithMany()
               .HasForeignKey(h => h.MateriaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Periodo)
               .WithMany()
               .HasForeignKey(h => h.PeriodoId)
               .OnDelete(DeleteBehavior.Restrict);

        // Relación con el usuario que auditó la nota
        builder.HasOne(h => h.Auditor)
               .WithMany()
               .HasForeignKey(h => h.RegistradoPor)
               .OnDelete(DeleteBehavior.SetNull); // Si borramos al docente, la nota queda (no se pierde), pero sin autor
    }
}