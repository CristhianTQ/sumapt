using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Analitica;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Analitica;

/// <summary>
/// Configuración Fluent API para la tabla analitica.predicciones_riesgo.
/// </summary>
public class PrediccionRiesgoConfiguration : IEntityTypeConfiguration<PrediccionRiesgo>
{
    public void Configure(EntityTypeBuilder<PrediccionRiesgo> builder)
    {
        builder.ToTable("predicciones_riesgo", "analitica");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");

        // Precisión: 5 dígitos totales, 4 decimales (Ej. 0.9854)
        builder.Property(p => p.ScoreRiesgo).HasColumnType("numeric(5,4)").IsRequired();
        builder.Property(p => p.NivelRiesgo).HasMaxLength(20).IsRequired();
        builder.Property(p => p.VersionModelo).HasMaxLength(50).IsRequired();
        
        // Mapeo JSONB para los factores clave de la predicción
        builder.Property(p => p.Factores).HasColumnType("jsonb").IsRequired();

        // Constraint Crítico: La probabilidad matemática debe estar entre 0 y 1
        builder.ToTable(t => t.HasCheckConstraint("CK_Score_Rango", "\"ScoreRiesgo\" >= 0 AND \"ScoreRiesgo\" <= 1"));

        // Relación con la inscripción
        builder.HasOne(p => p.Inscripcion)
               .WithMany()
               .HasForeignKey(p => p.InscripcionId)
               .OnDelete(DeleteBehavior.Cascade);

        // Índice para recuperar la última predicción de un estudiante rápidamente
        builder.HasIndex(p => new { p.InscripcionId, p.GeneradoEn }).IsDescending(false, true);
    }
}