using FluentValidation;

namespace SUMAPT.Application.Comunicacion.Commands.IniciarConversacion;

public class IniciarConversacionValidator : AbstractValidator<IniciarConversacionCommand>
{
    public IniciarConversacionValidator()
    {
        RuleFor(x => x.IniciadorId).NotEmpty().WithMessage("El ID del iniciador es obligatorio.");
        RuleFor(x => x.ReceptorId).NotEmpty().WithMessage("El ID del receptor es obligatorio.");
        
        RuleFor(x => x)
            .Must(x => x.IniciadorId != x.ReceptorId)
            .WithMessage("No se puede iniciar un chat con uno mismo.");
    }
}