using GiganteDeAco.Application.Handlers;
using GiganteDeAco.Contracts.Commands.Cabecas.Inclinacao;
using GiganteDeAco.Contracts.Commands.Cabecas.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GiganteDeAco.WebApi.Controllers;

[ApiController]
[Route("api/cabeca")]
public class CabecaController(ICabecaHandler cabecaHandler) : ControllerBase
{
    private readonly ICabecaHandler _cabecaHandler = cabecaHandler;

    [HttpPut("inclinacao/avancar")]
    public async Task<ObterRoboResponse> AvancarInclinacao()
    {
        var request = new AvancarInclinacaoCabecaRequest();
        return await _cabecaHandler.HandleAsync(request);
    }

    [HttpPut("inclinacao/voltar")]
    public async Task<ObterRoboResponse> VoltarInclinacao()
    {
        var request = new VoltarInclinacaoCabecaRequest();
        return await _cabecaHandler.HandleAsync(request);
    }

    [HttpPut("rotacao/avancar")]
    public async Task<ObterRoboResponse> AvancarRotacao()
    {
        var request = new AvancarRotacaoCabecaRequest();
        return await _cabecaHandler.HandleAsync(request);
    }

    [HttpPut("rotacao/voltar")]
    public async Task<ObterRoboResponse> VoltarRotacao()
    {
        var request = new VoltarRotacaoCabecaRequest();
        return await _cabecaHandler.HandleAsync(request);
    }
}