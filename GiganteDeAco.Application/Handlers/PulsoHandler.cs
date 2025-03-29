using GiganteDeAco.Application.Mappers;
using GiganteDeAco.Contracts.Commands.Pulsos.Rotacao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Handlers;
using GiganteDeAco.Domain.Repositories;
using GiganteDeAco.Domain.Rules;
using GiganteDeAco.Domain.Validators;

namespace GiganteDeAco.Application.Handlers;

public class PulsoHandler(IRoboRepository roboRepository) : IPulsoHandler
{
    private readonly IRoboRepository _roboRepository = roboRepository;

    public async Task<ObterRoboResponse> HandleAsync(AvancarRotacaoPulsoRequest request)
    {
        var response = new ObterRoboResponse();

        RotacaoPulsoValidator.Validar(request, response);
        if (!response.IsValid())
            return response;

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        var braco = (Lado)request.Lado == Lado.Direito ? robo.BracoDireito : robo.BracoEsquerdo;

        RotacaoPulsoRule.ValidarAvancar(braco, response);
        if (!response.IsValid())
            return response;

        braco.Pulso.EtapaRotacao.Avancar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(VoltarRotacaoPulsoRequest request)
    {
        var response = new ObterRoboResponse();

        RotacaoPulsoValidator.Validar(request, response);
        if (!response.IsValid())
            return response;

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        var braco = (Lado)request.Lado == Lado.Direito ? robo.BracoDireito : robo.BracoEsquerdo;

        RotacaoPulsoRule.ValidarVoltar(braco, response);
        if (!response.IsValid())
            return response;

        braco.Pulso.EtapaRotacao.Voltar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }
}