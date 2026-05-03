using FluentValidation;

namespace SUMAPT.Application.Mentoria.Commands.CrearPerfilMentor;

/// <summary>
/// Validación de frontera para evitar perfiles de mentor incompletos o corruptos.
/// </summary>
public class CrearPerfilMentorValidator : AbstractValidator<CrearPerfilMentorCommand>
{
    public CrearPerfilMentorValidator()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID del usuario es obligatorio.");

        RuleFor(x => x.Biografia)
            .MaximumLength(1000).WithMessage("La biografía no puede exceder los 1000 caracteres.");

        RuleFor(x => x.Especialidades)
            .NotNull().WithMessage("Se requiere al menos enviar una lista vacía de especialidades.");

        RuleFor(x => x.MaxEstudiantes)
            .GreaterThan((short)0).WithMessage("Debe aceptar al menos a 1 estudiante para ser mentor.");
    }
}