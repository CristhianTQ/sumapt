using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Analitica;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Analitica;

/// <summary>
/// Configuración Fluent API para la tabla analitica.metricas_agregadas.
/// </summary>
public class MetricaAgregadaConfiguration : IEntityTypeConfiguration<MetricaAgregada>
{
    public void Configure(EntityTypeBuilder<MetricaAgregada> builder)
    {
        builder.ToTable("metricas_agregadas", "analitica");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(m => m.TipoMetrica).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Valor).HasColumnType("numeric(10,4)").IsRequired();

        // Relaciones
        builder.HasOne(m => m.Institucion)
               .WithMany()
               .HasForeignKey(m => m.InstitucionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}