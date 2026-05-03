using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearPeriodo;

/// <summary>
/// Validación de frontera para el Periodo Académico antes de tocar la BD.
/// </summary>
public class CrearPeriodoValidator : AbstractValidator<CrearPeriodoCommand>
{
    public CrearPeriodoValidator()
    {
        RuleFor(x => x.InstitucionId)
            .NotEmpty().WithMessage("El ID de la Institución es obligatorio.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del periodo es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.FechaInicio)
            .NotEmpty().WithMessage("La fecha de inicio es obligatoria.");

        RuleFor(x => x.FechaFin)
            .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
            .GreaterThan(x => x.FechaInicio).WithMessage("La fecha de finalización no puede ser anterior ni igual a la fecha de inicio.");
    }
}