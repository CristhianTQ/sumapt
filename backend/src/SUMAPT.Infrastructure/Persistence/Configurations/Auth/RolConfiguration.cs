using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// Configuración Fluent API para la tabla auth.roles.
/// </summary>
public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles", "auth");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(r => r.Nombre).HasMaxLength(50).IsRequired();
        
        // El nombre del rol debe ser único a nivel sistema
        builder.HasIndex(r => r.Nombre).IsUnique();
    }
}