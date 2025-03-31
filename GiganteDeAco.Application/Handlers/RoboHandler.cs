using GiganteDeAco.Application.Mappers;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Commands.Robos.Resetar;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Handlers;
using GiganteDeAco.Domain.Repositories;

namespace GiganteDeAco.Application.Handlers;

public class RoboHandler(IRoboRepository roboRepository) : IRoboHandler
{
    private readonly IRoboRepository _roboRepository = roboRepository;

    public async Task<ObterRoboResponse> HandleAsync(ObterRoboRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(ResetarRoboRequest request)
    {
        var response = new ObterRoboResponse();

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        ResetarRotacaoCabeca(robo, response);
        if (!response.IsValid())
            return response;

        ResetarInclinacaoCabeca(robo, response);
        if (!response.IsValid())
            return response;

        ResetarRotacaoPulso(robo, response);
        if (!response.IsValid())
            return response;

        ResetarContracaoCotovelo(robo, response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    private static void ResetarRotacaoCabeca(Robo robo, Response response)
    {
        while (robo.Cabeca.Rotacao != RotacaoCabeca.EmRepouso)
        {
            if (robo.Cabeca.Rotacao > RotacaoCabeca.EmRepouso)
                robo.Cabeca.EtapaRotacao.Voltar(response);
            else
                robo.Cabeca.EtapaRotacao.Avancar(response);

            if (!response.IsValid())
                return;
        }
    }

    private static void ResetarInclinacaoCabeca(Robo robo, Response response)
    {
        while (robo.Cabeca.Inclinacao != InclinacaoCabeca.EmRepouso)
        {
            if (robo.Cabeca.Inclinacao > InclinacaoCabeca.EmRepouso)
                robo.Cabeca.EtapaInclinacao.Voltar(response);
            else
                robo.Cabeca.EtapaInclinacao.Avancar(response);

            if (!response.IsValid())
                return;
        }
    }

    private static void ResetarRotacaoPulso(Robo robo, Response response)
    {
        foreach (var braco in robo.Bracos)
        {
            while (braco.Pulso.Rotacao != RotacaoPulso.EmRepouso)
            {
                if (braco.Pulso.Rotacao > RotacaoPulso.EmRepouso)
                    braco.Pulso.EtapaRotacao.Voltar(response);
                else
                    braco.Pulso.EtapaRotacao.Avancar(response);

                if (!response.IsValid())
                    return;
            }

            if (!response.IsValid())
                return;
        }
    }

    private static void ResetarContracaoCotovelo(Robo robo, Response response)
    {
        foreach (var braco in robo.Bracos)
        {
            while (braco.Cotovelo.Contracao != ContracaoCotovelo.EmRepouso)
            {
                if (braco.Cotovelo.Contracao > ContracaoCotovelo.EmRepouso)
                    braco.Cotovelo.EtapaContracao.Voltar(response);
                else
                    braco.Cotovelo.EtapaContracao.Avancar(response);

                if (!response.IsValid())
                    return;
            }

            if (!response.IsValid())
                return;
        }
    }
}