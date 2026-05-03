using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Academico;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Academico;

/// <summary>
/// Configuración Fluent API para la tabla academico.periodos.
/// </summary>
public class PeriodoConfiguration : IEntityTypeConfiguration<Periodo>
{
    public void Configure(EntityTypeBuilder<Periodo> builder)
    {
        builder.ToTable("periodos", "academico");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Nombre).HasMaxLength(100).IsRequired();

        // El driver Npgsql (Postgres) mapeará automáticamente DateOnly al tipo DATE nativo de SQL
        builder.Property(p => p.FechaInicio).HasColumnType("date").IsRequired();
        builder.Property(p => p.FechaFin).HasColumnType("date").IsRequired();

        builder.HasOne(p => p.Institucion)
               .WithMany()
               .HasForeignKey(p => p.InstitucionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}