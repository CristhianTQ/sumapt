using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Auth;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Auth;

/// <summary>
/// Configuración Fluent API para la tabla auth.usuarios.
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        // Esquema y Tabla
        builder.ToTable("usuarios", "auth");

        // Llave Primaria
        builder.HasKey(u => u.Id);
        
        // Uso de la función nativa de Postgres para UUIDs por defecto
        builder.Property(u => u.Id)
               .HasDefaultValueSql("gen_random_uuid()");

        // Restricciones y longitudes máximas
        builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
        builder.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Apellido).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Telefono).HasMaxLength(20);
        builder.Property(u => u.ZonaHoraria).HasMaxLength(60).IsRequired();
        builder.Property(u => u.Idioma).HasMaxLength(10).IsRequired();

        // Índices y Unicidad dictados en el Diccionario SQL
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.KeycloakId).IsUnique();
    }
}