using FluentValidation;

namespace SUMAPT.Application.Mentoria.Commands.RegistrarActaSesion;

/// <summary>
/// Validador de frontera para asegurar la calidad de la data del acta.
/// </summary>
public class RegistrarActaSesionValidator : AbstractValidator<RegistrarActaSesionCommand>
{
    public RegistrarActaSesionValidator()
    {
        RuleFor(x => x.CitaId)
            .NotEmpty().WithMessage("El ID de la cita es obligatorio.");

        RuleFor(x => x.TemasTratados)
            .NotEmpty().WithMessage("Debe detallar los temas tratados durante la sesión.")
            .MinimumLength(10).WithMessage("El resumen de temas tratados es demasiado corto.");

        RuleFor(x => x.NivelRiesgoPercibido)
            .MaximumLength(20).WithMessage("El nivel de riesgo no puede exceder los 20 caracteres.");
    }
}