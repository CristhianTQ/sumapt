using FluentValidation;

namespace SUMAPT.Application.Auth.Commands.SincronizarUsuarioKeycloak;

/// <summary>
/// Reglas de validación estrictas que se ejecutarán automáticamente ANTES de que el Handler actúe.
/// Protege a la base de datos de datos corruptos o maliciosos.
/// </summary>
public class SincronizarUsuarioKeycloakValidator : AbstractValidator<SincronizarUsuarioKeycloakCommand>
{
    public SincronizarUsuarioKeycloakValidator()
    {
        RuleFor(x => x.KeycloakId)
            .NotEmpty().WithMessage("El identificador de Keycloak es obligatorio.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
            .MaximumLength(255).WithMessage("El correo excede la longitud máxima permitida.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre excede los 100 caracteres.");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(100).WithMessage("El apellido excede los 100 caracteres.");
    }
}