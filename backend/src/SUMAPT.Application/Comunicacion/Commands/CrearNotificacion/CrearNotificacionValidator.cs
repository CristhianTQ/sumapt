using FluentValidation;

namespace SUMAPT.Application.Comunicacion.Commands.CrearNotificacion;

/// <summary>
/// Validación de frontera para evitar el envío de alertas vacías o malformadas.
/// </summary>
public class CrearNotificacionValidator : AbstractValidator<CrearNotificacionCommand>
{
    public CrearNotificacionValidator()
    {
        RuleFor(x => x.DestinatarioId).NotEmpty().WithMessage("El ID del destinatario es obligatorio.");
        
        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("El tipo de notificación es obligatorio.")
            .MaximumLength(50).WithMessage("El tipo no puede exceder los 50 caracteres.");
            
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(255).WithMessage("El título no puede exceder los 255 caracteres.");
            
        RuleFor(x => x.Cuerpo)
            .NotEmpty().WithMessage("El cuerpo del mensaje no puede estar vacío.");
    }
}