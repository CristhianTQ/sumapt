using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SUMAPT.Domain.Entities.Mentoria;

namespace SUMAPT.Infrastructure.Persistence.Configurations.Mentoria;

/// <summary>
/// Configuración Fluent API para la tabla mentoria.perfiles_mentor.
/// </summary>
public class PerfilMentorConfiguration : IEntityTypeConfiguration<PerfilMentor>
{
    public void Configure(EntityTypeBuilder<PerfilMentor> builder)
    {
        builder.ToTable("perfiles_mentor", "mentoria");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");

        // Relación 1:1 con Usuario (Asegurada mediante Índice Único)
        builder.HasIndex(p => p.UsuarioId).IsUnique();

        builder.HasOne(p => p.Usuario)
               .WithOne()
               .HasForeignKey<PerfilMentor>(p => p.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade); // Si se borra el usuario, se borra su perfil de mentor
    }
}