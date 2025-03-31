using GiganteDeAco.Contracts.Commands.Cotovelos.Contracao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;

namespace GiganteDeAco.Domain.Handlers;

public interface ICotoveloHandler
{
    Task<ObterRoboResponse> HandleAsync(AvancarContracaoCotoveloRequest request);
    Task<ObterRoboResponse> HandleAsync(VoltarContracaoCotoveloRequest request);
}