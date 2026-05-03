using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.programas.
/// </summary>
public class ProgramaConfiguration : IEntityTypeConfiguration<Programa>
{
    public void Configure(EntityTypeBuilder<Programa> builder)
    {
        builder.ToTable("programas", "academico");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Nombre).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Codigo).HasMaxLength(50);

        // Relaciones
        builder.HasOne(p => p.Institucion)
               .WithMany()
               .HasForeignKey(p => p.InstitucionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ModeloAcademico)
               .WithMany()
               .HasForeignKey(p => p.ModeloId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}