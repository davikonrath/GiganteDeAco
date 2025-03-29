using GiganteDeAco.Contracts.Commands.Pulsos.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;

namespace GiganteDeAco.Domain.Handlers;

public interface IPulsoHandler
{
    Task<ObterRoboResponse> HandleAsync(AvancarRotacaoPulsoRequest request);
    Task<ObterRoboResponse> HandleAsync(VoltarRotacaoPulsoRequest request);
}