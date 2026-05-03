using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearInscripcion;

/// <summary>
/// Validación de frontera para evitar el procesamiento de IDs nulos o vacíos.
/// </summary>
public class CrearInscripcionValidator : AbstractValidator<CrearInscripcionCommand>
{
    public CrearInscripcionValidator()
    {
        RuleFor(x => x.EstudianteId)
            .NotEmpty().WithMessage("El ID del estudiante es obligatorio.");

        RuleFor(x => x.ProgramaId)
            .NotEmpty().WithMessage("El ID del programa es obligatorio.");

        RuleFor(x => x.PeriodoId)
            .NotEmpty().WithMessage("El ID del periodo académico es obligatorio.");
    }
}