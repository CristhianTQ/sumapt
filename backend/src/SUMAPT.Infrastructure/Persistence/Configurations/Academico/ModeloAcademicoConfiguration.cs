using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.modelos_academicos.
/// </summary>
public class ModeloAcademicoConfiguration : IEntityTypeConfiguration<ModeloAcademico>
{
    public void Configure(EntityTypeBuilder<ModeloAcademico> builder)
    {
        builder.ToTable("modelos_academicos", "academico");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(m => m.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Tipo).HasMaxLength(50).IsRequired();

        // Relación con Institución (DeleteBehavior.Restrict para evitar borrar la universidad por error)
        builder.HasOne(m => m.Institucion)
               .WithMany()
               .HasForeignKey(m => m.InstitucionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}