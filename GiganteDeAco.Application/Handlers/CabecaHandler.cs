using GiganteDeAco.Application.Mappers;
using GiganteDeAco.Contracts.Commands.Cabecas.Inclinacao;
using GiganteDeAco.Contracts.Commands.Cabecas.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Handlers;
using GiganteDeAco.Domain.Repositories;
using GiganteDeAco.Domain.Rules;

namespace GiganteDeAco.Application.Handlers;

public class CabecaHandler(IRoboRepository roboRepository) : ICabecaHandler
{
    private readonly IRoboRepository _roboRepository = roboRepository;

    public async Task<ObterRoboResponse> HandleAsync(AvancarInclinacaoCabecaRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        InclinacaoCabecaRule.ValidarAvancar(robo, response);
        if (!response.IsValid())
            return response;

        robo.Cabeca.EtapaInclinacao.Avancar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(VoltarInclinacaoCabecaRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        robo.Cabeca.EtapaInclinacao.Voltar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(AvancarRotacaoCabecaRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        RotacaoCabecaRule.ValidarAvancar(robo, response);
        if (!response.IsValid())
            return response;

        robo.Cabeca.EtapaRotacao.Avancar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(VoltarRotacaoCabecaRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        RotacaoCabecaRule.ValidarVoltar(robo, response);
        if (!response.IsValid())
            return response;

        robo.Cabeca.EtapaRotacao.Voltar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }
}