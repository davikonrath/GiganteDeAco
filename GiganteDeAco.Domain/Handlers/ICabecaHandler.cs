using GiganteDeAco.Contracts.Commands.Cabecas.Inclinacao;
using GiganteDeAco.Contracts.Commands.Cabecas.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;

namespace GiganteDeAco.Domain.Handlers;

public interface ICabecaHandler
{
    Task<ObterRoboResponse> HandleAsync(AvancarInclinacaoCabecaRequest request);
    Task<ObterRoboResponse> HandleAsync(VoltarInclinacaoCabecaRequest request);
    Task<ObterRoboResponse> HandleAsync(AvancarRotacaoCabecaRequest request);
    Task<ObterRoboResponse> HandleAsync(VoltarRotacaoCabecaRequest request);
}