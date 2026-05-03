using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SUMAPT.Application;

/// <summary>
/// Contenedor de Inyección de Dependencias para la Capa de Casos de Uso.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra automáticamente todos los validadores de FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Registra MediatR y le dice que busque todos los Handlers en este proyecto
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // TODO: Aquí registraremos los comportamientos (Behaviors) de Validación y Logging más adelante
        });

        return services;
    }
}