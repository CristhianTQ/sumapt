using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// Configuración Fluent API para la tabla auth.usuario_roles.
/// Gestiona la relación N:M y el multi-tenant (Institución).
/// </summary>
public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.ToTable("usuario_roles", "auth");

        builder.HasKey(ur => ur.Id);
        builder.Property(ur => ur.Id).HasDefaultValueSql("gen_random_uuid()");

        // Relaciones Foreign Keys con eliminación en cascada para el usuario
        builder.HasOne(ur => ur.Usuario)
               .WithMany()
               .HasForeignKey(ur => ur.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Rol)
               .WithMany()
               .HasForeignKey(ur => ur.RolId)
               .OnDelete(DeleteBehavior.Restrict);

        // Constraint compuesta de Unicidad (Regla de negocio: Un usuario no puede tener el mismo rol 2 veces en la misma institución)
        builder.HasIndex(ur => new { ur.UsuarioId, ur.RolId, ur.InstitucionId }).IsUnique();
    }
}