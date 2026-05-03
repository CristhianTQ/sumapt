using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearMateria;

/// <summary>
/// Reglas defensivas para proteger la consistencia de las materias curriculares.
/// </summary>
public class CrearMateriaValidator : AbstractValidator<CrearMateriaCommand>
{
    public CrearMateriaValidator()
    {
        RuleFor(x => x.ProgramaId)
            .NotEmpty().WithMessage("El ID del programa es obligatorio.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la materia es obligatorio.")
            .MaximumLength(255).WithMessage("El nombre no puede exceder los 255 caracteres.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El código de la materia es obligatorio.")
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres.");

        RuleFor(x => x.Creditos)
            .GreaterThan((short)0).WithMessage("La materia debe tener al menos 1 crédito.");

        RuleFor(x => x.PeriodoSugerido)
            .GreaterThan((short)0).When(x => x.PeriodoSugerido.HasValue)
            .WithMessage("Si se especifica un periodo sugerido, este debe ser mayor a cero.");
    }
}