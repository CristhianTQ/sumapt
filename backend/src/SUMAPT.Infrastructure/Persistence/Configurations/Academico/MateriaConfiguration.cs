using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.materias.
/// </summary>
public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder)
    {
        builder.ToTable("materias", "academico");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(m => m.Nombre).HasMaxLength(255).IsRequired();
        builder.Property(m => m.Codigo).HasMaxLength(50).IsRequired();

        // Relación con el Programa
        builder.HasOne(m => m.Programa)
               .WithMany()
               .HasForeignKey(m => m.ProgramaId)
               .OnDelete(DeleteBehavior.Cascade);

        // Regla de Negocio Crítica: No pueden existir dos materias con el mismo código en el mismo programa
        builder.HasIndex(m => new { m.ProgramaId, m.Codigo }).IsUnique();
    }
}