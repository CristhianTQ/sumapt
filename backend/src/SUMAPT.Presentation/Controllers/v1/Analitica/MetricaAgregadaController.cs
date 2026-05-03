using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUMAPT.Application.Analitica.Commands.RegistrarMetrica;

namespace SUMAPT.Presentation.Controllers.v1.Analitica;

/// <summary>
/// Controlador destinado a la inyección de KPIs y variables estadísticas globales.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MetricaAgregadaController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>Inicializa el orquestador de despachos CQRS.</summary>
    public MetricaAgregadaController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Guarda un valor estadístico precalculado para su rápida visualización en Dashboards.
    /// </summary>
    /// <param name="command">Datos de la métrica (Institución, Periodo, Tipo, Valor).</param>
    /// <returns>El ID único del KPI registrado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarMetricaCommand command)
    {
        var metricaId = await _mediator.Send(command);
        return Created(string.Empty, metricaId);
    }
}