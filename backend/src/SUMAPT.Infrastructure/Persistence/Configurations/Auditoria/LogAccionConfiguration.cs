using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Auditoria;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Auditoria;

/// <summary>
/// Configuración Fluent API para la tabla auditoria.log_acciones (La Caja Negra).
/// </summary>
public class LogAccionConfiguration : IEntityTypeConfiguration<LogAccion>
{
    public void Configure(EntityTypeBuilder<LogAccion> builder)
    {
        builder.ToTable("log_acciones", "auditoria");

        // Identidad Numérica (BIGSERIAL) para alta concurrencia
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).UseIdentityByDefaultColumn();

        builder.Property(l => l.RolActivo).HasMaxLength(50);
        builder.Property(l => l.Accion).HasMaxLength(100).IsRequired();
        builder.Property(l => l.EntidadTipo).HasMaxLength(100).IsRequired();
        builder.Property(l => l.Resultado).HasMaxLength(20).IsRequired();
        
        // Mapeo JSONB para el historial "Antes y Después"
        builder.Property(l => l.DatosAntes).HasColumnType("jsonb");
        builder.Property(l => l.DatosDespues).HasColumnType("jsonb");

        // Índices críticos para el Dashboard de Seguridad
        builder.HasIndex(l => new { l.UsuarioId, l.EjecutadoEn }).IsDescending(false, true);
        builder.HasIndex(l => new { l.EntidadTipo, l.EntidadId });
    }
}