using FluentValidation;

namespace SUMAPT.Application.Analitica.Commands.RegistrarMetrica;

/// <summary>
/// Validación de frontera para la ingesta de KPIs en los dashboards.
/// </summary>
public class RegistrarMetricaValidator : AbstractValidator<RegistrarMetricaCommand>
{
    public RegistrarMetricaValidator()
    {
        RuleFor(x => x.InstitucionId).NotEmpty().WithMessage("El ID de la institución es obligatorio.");
        
        RuleFor(x => x.TipoMetrica)
            .NotEmpty().WithMessage("Debe especificar el nombre o clave de la métrica.")
            .MaximumLength(100).WithMessage("La clave de la métrica no puede exceder los 100 caracteres.");
    }
}