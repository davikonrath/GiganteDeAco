using GiganteDeAco.Contracts.Commands.Cotovelos.Contracao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GiganteDeAco.WebApi.Controllers;

[ApiController]
[Route("api/cotovelo")]
public class CotoveloController(ICotoveloHandler cotoveloHandler) : ControllerBase
{
    private readonly ICotoveloHandler _cotoveloHandler = cotoveloHandler;

    [HttpPut("contracao/avancar")]
    public async Task<ObterRoboResponse> AvancarContracao([FromQuery] AvancarContracaoCotoveloRequest request)
    {
        return await _cotoveloHandler.HandleAsync(request);
    }

    [HttpPut("contracao/voltar")]
    public async Task<ObterRoboResponse> VoltarContracao([FromQuery] VoltarContracaoCotoveloRequest request)
    {
        return await _cotoveloHandler.HandleAsync(request);
    }
}