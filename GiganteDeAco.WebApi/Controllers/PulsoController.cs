using GiganteDeAco.Contracts.Commands.Pulsos.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GiganteDeAco.WebApi.Controllers;

[ApiController]
[Route("api/pulso")]
public class PulsoController(IPulsoHandler pulsoHandler) : ControllerBase
{
    private readonly IPulsoHandler _pulsoHandler = pulsoHandler;

    [HttpPut("rotacao/avancar")]
    public async Task<ObterRoboResponse> AvancarRotacaao([FromQuery] AvancarRotacaoPulsoRequest request)
    {
        return await _pulsoHandler.HandleAsync(request);
    }

    [HttpPut("rotacao/voltar")]
    public async Task<ObterRoboResponse> VoltarRotacao([FromQuery] VoltarRotacaoPulsoRequest request)
    {
        return await _pulsoHandler.HandleAsync(request);
    }
}