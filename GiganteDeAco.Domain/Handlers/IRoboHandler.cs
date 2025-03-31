using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Commands.Robos.Resetar;

namespace GiganteDeAco.Domain.Handlers;

public interface IRoboHandler
{
    Task<ObterRoboResponse> HandleAsync(ObterRoboRequest request);
    Task<ObterRoboResponse> HandleAsync(ResetarRoboRequest request);
}