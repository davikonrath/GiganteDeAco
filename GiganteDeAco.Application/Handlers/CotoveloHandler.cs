using GiganteDeAco.Application.Mappers;
using GiganteDeAco.Contracts.Commands.Cotovelos.Contracao;
using GiganteDeAco.Contracts.Commands.Robos.Obter;
using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Enums;
using GiganteDeAco.Domain.Handlers;
using GiganteDeAco.Domain.Repositories;
using GiganteDeAco.Domain.Rules;
using GiganteDeAco.Domain.Validators;

namespace GiganteDeAco.Application.Handlers;

public class CotoveloHandler(IRoboRepository roboRepository) : ICotoveloHandler
{
    private readonly IRoboRepository _roboRepository = roboRepository;

    public async Task<ObterRoboResponse> HandleAsync(AvancarContracaoCotoveloRequest request)
    {
        var response = new ObterRoboResponse();

        ContracaoCotoveloValidator.Validar(request, response);
        if (!response.IsValid())
            return response;

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        var braco = (Lado)request.Lado == Lado.Direito ? robo.BracoDireito : robo.BracoEsquerdo;

        braco.Cotovelo.EtapaContracao.Avancar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }

    public async Task<ObterRoboResponse> HandleAsync(VoltarContracaoCotoveloRequest request)
    {
        var response = new ObterRoboResponse();

        ContracaoCotoveloValidator.Validar(request, response);
        if (!response.IsValid())
            return response;

        var robo = await _roboRepository.ObterRobo();
        if (robo == null)
        {
            response.AddNotificacao(new NotificacaoNaoEncontrado(nameof(Robo)));
            return response;
        }

        var braco = (Lado)request.Lado == Lado.Direito ? robo.BracoDireito : robo.BracoEsquerdo;

        ContracaoCotoveloRule.ValidarVoltar(braco, response);
        if (!response.IsValid())
            return response;

        braco.Cotovelo.EtapaContracao.Voltar(response);
        if (!response.IsValid())
            return response;

        response.Robo = RoboMapper.Map(robo);
        return response;
    }
}