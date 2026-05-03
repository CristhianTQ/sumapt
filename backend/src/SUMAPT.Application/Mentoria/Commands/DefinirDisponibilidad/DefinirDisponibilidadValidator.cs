using FluentValidation;

namespace SUMAPT.Application.Mentoria.Commands.DefinirDisponibilidad;

/// <summary>
/// Validador de frontera para la asignación de horarios de mentoría.
/// </summary>
public class DefinirDisponibilidadValidator : AbstractValidator<DefinirDisponibilidadCommand>
{
    public DefinirDisponibilidadValidator()
    {
        RuleFor(x => x.MentorId)
            .NotEmpty().WithMessage("El ID del mentor es obligatorio.");

        RuleFor(x => x.DiaSemana)
            .InclusiveBetween((short)0, (short)6).WithMessage("El día de la semana debe estar entre 0 y 6.");

        RuleFor(x => x.Modalidad)
            .NotEmpty().WithMessage("La modalidad es obligatoria.")
            .MaximumLength(20).WithMessage("La modalidad no puede exceder los 20 caracteres.");

        // FluentValidation permite validaciones complejas entre propiedades
        RuleFor(x => x.HoraFin)
            .GreaterThan(x => x.HoraInicio)
            .WithMessage("La hora de fin debe ser posterior a la hora de inicio.");
    }
}