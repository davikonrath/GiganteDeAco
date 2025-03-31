using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Commands.Robos.Resetar;
using GiganteDeAco.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GiganteDeAco.WebApi.Controllers;

[ApiController]
[Route("api")]
public class RoboController(IRoboHandler roboHandler) : ControllerBase
{
    private readonly IRoboHandler _roboHandler = roboHandler;

    [HttpGet("robo")]
    public async Task<ObterRoboResponse> ObterRobo()
    {
        var request = new ObterRoboRequest();
        return await _roboHandler.HandleAsync(request);
    }

    [HttpPut("reset")]
    public async Task<ObterRoboResponse> ResetarRobo()
    {
        var request = new ResetarRoboRequest();
        return await _roboHandler.HandleAsync(request);
    }
}
