using FluentValidation;

namespace SUMAPT.Application.Academico.Commands.CrearInstitucion;

/// <summary>
/// Reglas de validación defensivas para proteger la tabla academico.instituciones.
/// </summary>
public class CrearInstitucionValidator : AbstractValidator<CrearInstitucionCommand>
{
    public CrearInstitucionValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la institución es obligatorio.")
            .MaximumLength(255).WithMessage("El nombre no puede exceder los 255 caracteres.");

        RuleFor(x => x.NombreCorto)
            .NotEmpty().WithMessage("El nombre corto o acrónimo es obligatorio.")
            .MaximumLength(50).WithMessage("El acrónimo no puede exceder los 50 caracteres.")
            .Matches("^[a-zA-Z0-9-]*$").WithMessage("El nombre corto solo puede contener letras, números y guiones.");

        RuleFor(x => x.DominioEmail)
            .MaximumLength(100).WithMessage("El dominio de email no puede exceder los 100 caracteres.");
            
        RuleFor(x => x.Pais)
            .MaximumLength(100).WithMessage("El país no puede exceder los 100 caracteres.");
    }
}