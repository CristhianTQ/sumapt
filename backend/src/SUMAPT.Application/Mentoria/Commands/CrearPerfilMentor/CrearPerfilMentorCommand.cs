using System;
using System.Collections.Generic;
using MediatR;

namespace SUMAPT.Application.Mentoria.Commands.CrearPerfilMentor;

/// <summary>
/// Comando inmutable para solicitar la creación de un perfil de mentoría.
/// </summary>
public record CrearPerfilMentorCommand(
    Guid UsuarioId,
    string? Biografia,
    List<string> Especialidades,
    short MaxEstudiantes
) : IRequest<Guid>;