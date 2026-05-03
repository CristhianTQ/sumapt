using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SUMAPT.Domain.Interfaces.Repositories;
using SUMAPT.Domain.Interfaces.Repositories.Auth;
using SUMAPT.Infrastructure.Persistence;
using SUMAPT.Infrastructure.Persistence.Repositories;
using SUMAPT.Infrastructure.Persistence.Repositories.Auth;

namespace SUMAPT.Infrastructure;

/// <summary>
/// Contenedor de Inyección de Dependencias para la Capa de Infraestructura.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuración de Base de Datos
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        // 2. Registro de Repositorios Genéricos y Específicos
        // AddScoped indica que se creará una instancia por cada petición HTTP
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}