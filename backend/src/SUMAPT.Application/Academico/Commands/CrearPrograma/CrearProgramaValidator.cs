using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearPrograma;

/// <summary>
/// Validador de frontera para la creación de un Programa Académico.
/// </summary>
public class CrearProgramaValidator : AbstractValidator<CrearProgramaCommand>
{
    public CrearProgramaValidator()
    {
        RuleFor(x => x.InstitucionId)
            .NotEmpty().WithMessage("La Institución es obligatoria.");

        RuleFor(x => x.ModeloId)
            .NotEmpty().WithMessage("El Modelo Académico es obligatorio.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del programa es obligatorio.")
            .MaximumLength(255).WithMessage("El nombre no puede exceder los 255 caracteres.");

        RuleFor(x => x.Codigo)
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres.");

        RuleFor(x => x.DuracionPeriodos)
            .GreaterThan(0).WithMessage("La duración del programa debe ser de al menos 1 periodo.");
    }
}