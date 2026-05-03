using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// Configuración Fluent API para la tabla auth.sesiones.
/// </summary>
public class SesionConfiguration : IEntityTypeConfiguration<Sesion>
{
    public void Configure(EntityTypeBuilder<Sesion> builder)
    {
        builder.ToTable("sesiones", "auth");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.KeycloakSid).HasMaxLength(255).IsRequired();
        
        // SID de Keycloak debe ser único para interceptar logouts
        builder.HasIndex(s => s.KeycloakSid).IsUnique();

        // Relación con el Usuario
        builder.HasOne(s => s.Usuario)
               .WithMany()
               .HasForeignKey(s => s.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

        // Índice optimizado para buscar sesiones activas rápidamente
        builder.HasIndex(s => s.UsuarioId)
               .HasFilter("\"Activa\" = TRUE");
    }
}