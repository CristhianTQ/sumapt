using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearModeloAcademico;

/// <summary>
/// Validación de frontera para el Modelo Académico.
/// </summary>
public class CrearModeloAcademicoValidator : AbstractValidator<CrearModeloAcademicoCommand>
{
    public CrearModeloAcademicoValidator()
    {
        RuleFor(x => x.InstitucionId)
            .NotEmpty().WithMessage("El ID de la Institución es obligatorio.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del modelo es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("El tipo de modelo es obligatorio.")
            .MaximumLength(50).WithMessage("El tipo no puede exceder los 50 caracteres.");

        RuleFor(x => x.PeriodosPorAño)
            .GreaterThan(0).WithMessage("Debe existir al menos 1 periodo por año.");
    }
}