using FluentValidation;

namespace SUMAPT.Application.Mentoria.Commands.ReservarCita;

/// <summary>
/// Validación de frontera para las reservas de mentoría.
/// </summary>
public class ReservarCitaValidator : AbstractValidator<ReservarCitaCommand>
{
    public ReservarCitaValidator()
    {
        RuleFor(x => x.MentorId).NotEmpty().WithMessage("El ID del mentor es obligatorio.");
        RuleFor(x => x.EstudianteId).NotEmpty().WithMessage("El ID del estudiante es obligatorio.");

        RuleFor(x => x.Modalidad)
            .NotEmpty().WithMessage("La modalidad es obligatoria.")
            .MaximumLength(20).WithMessage("La modalidad no puede exceder 20 caracteres.");

        RuleFor(x => x.FechaHoraIni).NotEmpty().WithMessage("Debe especificar la fecha y hora de inicio.");
        
        RuleFor(x => x.FechaHoraFin)
            .NotEmpty().WithMessage("Debe especificar la fecha y hora de finalización.")
            .GreaterThan(x => x.FechaHoraIni).WithMessage("La finalización debe ser posterior al inicio.");
    }
}