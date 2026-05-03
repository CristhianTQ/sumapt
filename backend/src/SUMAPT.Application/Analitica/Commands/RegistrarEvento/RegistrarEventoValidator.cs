using FluentValidation;

namespace SUMAPT.Application.Analitica.Commands.RegistrarEvento;

/// <summary>
/// Validador ligero para la telemetría. No debe ejecutar reglas costosas para no bloquear el flujo de la UI.
/// </summary>
public class RegistrarEventoValidator : AbstractValidator<RegistrarEventoCommand>
{
    public RegistrarEventoValidator()
    {
        RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El ID del usuario es obligatorio para la trazabilidad.");
        
        RuleFor(x => x.TipoEvento)
            .NotEmpty().WithMessage("Debe categorizar el tipo de evento.")
            .MaximumLength(100).WithMessage("El tipo de evento no puede exceder los 100 caracteres.");

        RuleFor(x => x.EntidadTipo)
            .MaximumLength(50).WithMessage("El tipo de entidad no puede exceder los 50 caracteres.");
    }
}