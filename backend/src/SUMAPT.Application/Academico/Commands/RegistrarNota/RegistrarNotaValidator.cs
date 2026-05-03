using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.RegistrarNota;

/// <summary>
/// Validación de frontera para el registro de calificaciones.
/// </summary>
public class RegistrarNotaValidator : AbstractValidator<RegistrarNotaCommand>
{
    public RegistrarNotaValidator()
    {
        RuleFor(x => x.InscripcionId).NotEmpty().WithMessage("El ID de la inscripción es obligatorio.");
        RuleFor(x => x.MateriaId).NotEmpty().WithMessage("El ID de la materia es obligatorio.");
        RuleFor(x => x.PeriodoId).NotEmpty().WithMessage("El ID del periodo es obligatorio.");

        RuleFor(x => x.NotaFinal)
            .InclusiveBetween(0, 100).When(x => x.NotaFinal.HasValue)
            .WithMessage("Si se envía una nota, debe estar entre 0 y 100.");

        RuleFor(x => x.Intentos)
            .GreaterThan((short)0).WithMessage("El número de intentos debe ser al menos 1.");
    }
}