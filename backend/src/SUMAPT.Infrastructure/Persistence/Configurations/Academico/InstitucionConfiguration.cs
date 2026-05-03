using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.instituciones.
/// </summary>
public class InstitucionConfiguration : IEntityTypeConfiguration<Institucion>
{
    public void Configure(EntityTypeBuilder<Institucion> builder)
    {
        builder.ToTable("instituciones", "academico");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasDefaultValueSql("gen_random_uuid()");

        // Mapeo de restricciones SQL
        builder.Property(i => i.Nombre).HasMaxLength(255).IsRequired();
        builder.Property(i => i.NombreCorto).HasMaxLength(50).IsRequired();
        builder.Property(i => i.DominioEmail).HasMaxLength(100);
        builder.Property(i => i.Pais).HasMaxLength(100);
        builder.Property(i => i.ZonaHoraria).HasMaxLength(60).IsRequired();

        // El acrónimo o nombre corto debe ser único
        builder.HasIndex(i => i.NombreCorto).IsUnique();
    }
}