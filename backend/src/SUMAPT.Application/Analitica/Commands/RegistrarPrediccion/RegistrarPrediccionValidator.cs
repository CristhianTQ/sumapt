using FluentValidation;

namespace SUMAPT.Application.Analitica.Commands.RegistrarPrediccion;

/// <summary>
/// Validaciones de frontera para garantizar la sanidad de los datos que inyecta el motor de Python.
/// </summary>
public class RegistrarPrediccionValidator : AbstractValidator<RegistrarPrediccionCommand>
{
    public RegistrarPrediccionValidator()
    {
        RuleFor(x => x.InscripcionId).NotEmpty().WithMessage("El ID de inscripción es obligatorio.");
        
        RuleFor(x => x.ScoreRiesgo)
            .InclusiveBetween(0, 1).WithMessage("El score probabilístico debe estar entre 0.00 y 1.00.");
            
        RuleFor(x => x.NivelRiesgo)
            .NotEmpty().WithMessage("Debe categorizar el nivel de riesgo.")
            .MaximumLength(20).WithMessage("El nivel de riesgo no puede exceder los 20 caracteres.");
            
        RuleFor(x => x.Factores).NotEmpty().WithMessage("Debe incluir el JSON de explicabilidad (Factores).");
        RuleFor(x => x.VersionModelo).NotEmpty().WithMessage("La versión del modelo es obligatoria para auditoría.");
        RuleFor(x => x.VigenteHasta).NotEmpty().WithMessage("Se debe especificar la vigencia de la inferencia.");
    }
}