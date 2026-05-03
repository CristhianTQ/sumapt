using FluentValidation;

namespace SUMAPT.Application.Auditoria.Commands.RegistrarLogAccion;

/// <summary>
/// Validación de frontera para el libro mayor de auditoría.
/// </summary>
public class RegistrarLogAccionValidator : AbstractValidator<RegistrarLogAccionCommand>
{
    public RegistrarLogAccionValidator()
    {
        RuleFor(x => x.Accion)
            .NotEmpty().WithMessage("La acción es obligatoria.")
            .MaximumLength(100).WithMessage("La acción no puede exceder los 100 caracteres.");

        RuleFor(x => x.EntidadTipo)
            .NotEmpty().WithMessage("El tipo de entidad es obligatorio.")
            .MaximumLength(100).WithMessage("El tipo de entidad no puede exceder los 100 caracteres.");
            
        RuleFor(x => x.Resultado)
            .MaximumLength(20).WithMessage("El resultado no puede exceder los 20 caracteres.");
    }
}