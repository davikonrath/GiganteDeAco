using GiganteDeAco.Contracts.Common;
using GiganteDeAco.Domain.Entities.Robos;
using GiganteDeAco.Domain.Entities.Workflow.Etapas.Cotovelos;

namespace GiganteDeAco.Domain.Rules;

public static class RotacaoPulsoRule
{
    public static void ValidarAvancar(Braco braco, Response response)
    {
        if (braco.Cotovelo.EtapaContracao is not EtapaContracaoCotoveloForte)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }

    public static void ValidarVoltar(Braco braco, Response response)
    {
        if (braco.Cotovelo.EtapaContracao is not EtapaContracaoCotoveloForte)
            response.AddNotificacao(new NotificacaoAcaoNaoPermitida());
    }
}