using FluentValidation;

namespace SUMAPT.Application.Comunicacion.Commands.EnviarMensaje;

/// <summary>
/// Validador de frontera para bloquear ráfagas de mensajes vacíos o excesivamente largos.
/// </summary>
public class EnviarMensajeValidator : AbstractValidator<EnviarMensajeCommand>
{
    public EnviarMensajeValidator()
    {
        RuleFor(x => x.ConversacionId)
            .NotEmpty().WithMessage("El ID de la conversación es obligatorio.");

        RuleFor(x => x.RemitenteId)
            .NotEmpty().WithMessage("El ID del remitente es obligatorio.");

        RuleFor(x => x.Contenido)
            .NotEmpty().WithMessage("El mensaje no puede estar vacío ni contener solo espacios en blanco.")
            .MaximumLength(4000).WithMessage("El mensaje excede el límite permitido de 4000 caracteres.");
    }
}